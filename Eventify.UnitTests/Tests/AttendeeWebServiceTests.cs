using Eventify.Common.Classes;
using Eventify.Common.Classes.Attendees;
using Eventify.Common.Utils.Exceptions;
using Eventify.Common.Utils.Logger;
using Eventify.Common.Utils.Messages;
using Eventify.Common.Utils.Validations;
using Eventify.Core.Attendees;
using Eventify.DAL.Attendees;
using Eventify.UnitTests.Extensions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;

namespace Eventify.UnitTests.Tests
{
    internal class AttendeeWebServiceTests : UnitTest
    {
        private IAttendeeWebService _attendeeWebService;

        private ILogger _logger;
        private IAttendeeRepository _attendeeRepository;
        private IAttendeeSaveValidator _attendeeSaveValidator;

        private EventAttendee _personEventAttendee;
        private EventAttendee _companyEventAttendee;

        private AttendeeViewModel _personEventAttendeeDetails;

        private AttendeeGridViewModel _companyEventAttendeeGrid;

        [SetUp]
        public void Setup()
        {
            _logger = Substitute.For<ILogger>();
            _attendeeRepository = Substitute.For<IAttendeeRepository>();
            _attendeeSaveValidator = Substitute.For<IAttendeeSaveValidator>();

            _attendeeWebService = new AttendeeWebService(_logger, _attendeeRepository, _attendeeSaveValidator);

            var now = DateTime.Now;
            var eventId = new Guid();
            var personEventAttendeeId = new Guid();
            var companyEventAttendeeId = new Guid();
            var personAttendeeId = new Guid();
            var companyAttendeeId = new Guid();

            _personEventAttendee = new EventAttendee
            {
                Id = personEventAttendeeId,
                AttendeeId = personAttendeeId,
                Attendee = new Attendee
                {
                    Id = personAttendeeId,
                    Name = "UnitTest1",
                    LastName = "UnitTest1",
                    Code = "49901115248",
                    AttendeeType = AttendeeType.Person
                },
                CreatedDate = now,
                EventId = eventId,
                Notes = "UnitTest1",
                PaymentMethod = "UnitTest1"
            };

            _companyEventAttendee = new EventAttendee
            {
                Id = companyEventAttendeeId,
                AttendeeId = companyAttendeeId,
                Attendee = new Attendee
                {
                    Name = "UnitTest2",
                    Code = "UnitTest2",
                    AttendeeType = AttendeeType.Company
                },
                CreatedDate = now,
                EventId = eventId,
                Notes = "UnitTest2",
                Participants = 100,
                PaymentMethod = "UnitTest2"
            };

            _personEventAttendeeDetails = new AttendeeViewModel
            {
                Id = personEventAttendeeId,
                Name = _personEventAttendee.Attendee.Name,
                LastName = _personEventAttendee.Attendee.LastName,
                Code = _personEventAttendee.Attendee.Code,
                Notes = _personEventAttendee.Notes,
                PaymentMethod = _personEventAttendee.PaymentMethod,
                AttendeeType = AttendeeType.Person,
                EventId = _personEventAttendee.EventId
            };

            _companyEventAttendeeGrid = new AttendeeGridViewModel
            {
                Id = companyEventAttendeeId,
                Code = _companyEventAttendee.Attendee.Code,
                FullName = _companyEventAttendee.Attendee.GetFullName()
            };
        }

        [Test]
        public async Task GetAttendee_returnsAttendeeDetails()
        {
            _attendeeRepository.GetEventAttendeeById(_personEventAttendee.Id).Returns(_personEventAttendee);

            var result = await _attendeeWebService.GetEventAttendee(_personEventAttendee.Id);

            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Data);
            AssertExtensions.AreEqualByJson(result.Data, _personEventAttendeeDetails);
        }

        [Test]
        public async Task GetAttendeeByInvalidId_returnsError()
        {
            var invalidId = Guid.Parse("00000000-0000-0000-0000-000000000000");
            _attendeeRepository.GetEventAttendeeById(invalidId).ReturnsNull();

            var result = await _attendeeWebService.GetEventAttendee(invalidId);
            var message = $"Event attendee with id {invalidId} was not found";

            Assert.IsFalse(result.Success);
            Assert.IsNull(result.Data);
            Assert.That(result.Messages, Has.Exactly(1).Matches<SimpleMessage>(x => x.Header == message));
        }

        [Test]
        public async Task AddAttendeeToEvent_returnsAttendeeGrid()
        {
            var saveModel = new AttendeeSaveModel
            {
                EventId = _companyEventAttendee.EventId,
                Name = _companyEventAttendee.Attendee.Name,
                LastName = _companyEventAttendee.Attendee.LastName,
                Code = _companyEventAttendee.Attendee.Code,
                PaymentMethod = _companyEventAttendee.PaymentMethod,
                Participants = _companyEventAttendee.Participants,
                Notes = _companyEventAttendee.Notes,
                AttendeeType = _companyEventAttendee.Attendee.AttendeeType
            };

            _attendeeSaveValidator.Validate(saveModel).Returns(new ValidationMessages());
            _attendeeRepository.AddEventAttendee(Arg.Any<EventAttendee>()).ReturnsForAnyArgs(_companyEventAttendee);

            var result = await _attendeeWebService.SaveAttendee(saveModel);

            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Data);
            AssertExtensions.AreEqualByJson(result.Data, _companyEventAttendeeGrid);
        }

        [Test]
        public async Task UpdateEventAttendee_returnsAttendeeDetails()
        {
            var saveModel = new AttendeeSaveModel
            {
                Id = _companyEventAttendee.Id,
                EventId = _companyEventAttendee.EventId,
                Name = _companyEventAttendee.Attendee.Name,
                LastName = _companyEventAttendee.Attendee.LastName,
                Code = _companyEventAttendee.Attendee.Code,
                PaymentMethod = _companyEventAttendee.PaymentMethod,
                Participants = _companyEventAttendee.Participants,
                Notes = _companyEventAttendee.Notes,
                AttendeeType = _companyEventAttendee.Attendee.AttendeeType
            };

            _attendeeSaveValidator.Validate(saveModel).Returns(new ValidationMessages());
            _attendeeRepository.UpdateEventAttendee(_companyEventAttendee.Id, Arg.Any<EventAttendeeUpdateSet>()).ReturnsForAnyArgs(_companyEventAttendee);

            var result = await _attendeeWebService.SaveAttendee(saveModel);

            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Data);
            AssertExtensions.AreEqualByJson(result.Data, _companyEventAttendeeGrid);
        }

        [Test]
        public async Task SaveEventAttendeeUnknownType_returnsValidationErrors()
        {
            var saveModel = new AttendeeSaveModel
            {
                AttendeeType = AttendeeType.Unknown
            };

            var attendeeSaveValidator = new AttendeeSaveValidator();
            var result = await attendeeSaveValidator.Validate(saveModel);

            var messages = result.GetWebMessages().ToList();

            Assert.IsFalse(result.IsValid);
            Assert.IsNotEmpty(messages);
            Assert.That(messages, Has.Exactly(1).Matches<SimpleMessage>(x => x.Header == ErrorMessages.AttendeeTypeInvalid));
        }


        [Test]
        public async Task SavePersonAttendeeInvalidData_returnsValidationErrors()
        {
            var saveModel = new AttendeeSaveModel
            {
                AttendeeType = AttendeeType.Person,
                Name = string.Empty,
                LastName = string.Empty,
                Code = "Invalid",
                PaymentMethod = string.Empty,
                Notes = new string('*', 1501)
            };

            var attendeeSaveValidator = new AttendeeSaveValidator();
            var result = await attendeeSaveValidator.Validate(saveModel);

            var messages = result.GetWebMessages().ToList();
            var expectedMessages = new[]
            {
                ErrorMessages.AttendeeFirstNameRequired,
                ErrorMessages.AttendeeLastNameRequired,
                ErrorMessages.AttendeePersonalCodeInvalid,
                ErrorMessages.AttendeePaymentMethodRequired,
                ErrorMessages.AttendeePersonNotesMaxLengthExceeded
            };

            Assert.IsFalse(result.IsValid);
            Assert.IsNotEmpty(messages);
            Assert.That(messages, Has.Exactly(5).Matches<SimpleMessage>(x => expectedMessages.Any(y => x.Header == y)));
        }

        [Test]
        public async Task SaveCompanyAttendeeInvalidData_returnsValidationErrors()
        {
            var saveModel = new AttendeeSaveModel
            {
                AttendeeType = AttendeeType.Company,
                Name = string.Empty,
                Code = string.Empty,
                Participants = 0,
                PaymentMethod = string.Empty,
                Notes = new string('*', 5001)
            };

            var attendeeSaveValidator = new AttendeeSaveValidator();
            var result = await attendeeSaveValidator.Validate(saveModel);

            var messages = result.GetWebMessages().ToList();
            var expectedMessages = new[]
            {
                ErrorMessages.AttendeeNameRequired,
                ErrorMessages.AttendeeRegisterCodeRequired,
                ErrorMessages.AttendeeParticipantsInvalid,
                ErrorMessages.AttendeePaymentMethodRequired,
                ErrorMessages.AttendeeCompanyNotesMaxLengthExceeded
            };

            Assert.IsFalse(result.IsValid);
            Assert.IsNotEmpty(messages);
            Assert.That(messages, Has.Exactly(5).Matches<SimpleMessage>(x => expectedMessages.Any(y => x.Header == y)));
        }

        [Test]
        public async Task  RemoveAttendeeFromEvent_returnsSuccess()
        {
            var result = await _attendeeWebService.RemoveAttendeeFromEvent(_personEventAttendee.Id);

            Assert.IsTrue(result.Success);
        }

        [Test]
        public async Task RemoveAttendeeFromEventInvalidId_returnsError()
        {
            var invalidId = Guid.Parse("00000000-0000-0000-0000-000000000000");
            var message = $"Event attendee with id {invalidId} does not exist";

            _attendeeRepository.DeleteEventAttendee(invalidId).Throws(new SimpleException(message));

            var result = await _attendeeWebService.RemoveAttendeeFromEvent(invalidId);

            Assert.IsFalse(result.Success);
            Assert.That(result.Messages, Has.Exactly(1).Matches<SimpleMessage>(x => x.Header == message));
        }
    }
}
