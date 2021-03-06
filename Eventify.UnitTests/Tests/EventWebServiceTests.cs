using Eventify.Common.Classes;
using Eventify.Common.Classes.Attendees;
using Eventify.Common.Classes.Events;
using Eventify.Common.Utils.Exceptions;
using Eventify.Common.Utils.Logger;
using Eventify.Common.Utils.Messages;
using Eventify.Common.Utils.Validations;
using Eventify.Core.Attendees;
using Eventify.Core.Events;
using Eventify.DAL.Events;
using Eventify.UnitTests.Extensions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;

namespace Eventify.UnitTests.Tests
{
    public class EventWebServiceTests : UnitTest
    {
        private IEventWebService _eventWebService;

        private ILogger _logger;
        private IEventRepository _eventRepository;
        private IEventSaveValidator _eventSaveValidator;

        private Event _futureEvent;
        private Event _pastEvent;

        private EventGridRowViewModel _futureEventGridRow;
        private EventGridRowViewModel _pastEventGridRow;

        private EventDetailsViewModel _futureEventDetails;

        [SetUp]
        public void Setup()
        {
            _logger = Substitute.For<ILogger>();
            _eventRepository = Substitute.For<IEventRepository>();
            _eventSaveValidator = Substitute.For<IEventSaveValidator>();

            _eventWebService = new EventWebService(_logger, _eventRepository, _eventSaveValidator);

            var futureEventId = new Guid();
            var pastEventId = new Guid();

            _futureEvent = new Event
            {
                Id = futureEventId,
                Address = "UnitTest1",
                Name = "UnitTest1",
                Notes = "UnitTest1",
                StartDate = new DateTime(2099, 1, 1, 0, 0, 0),
                EventAttendees = new List<EventAttendee>()
            };

            _futureEventGridRow = new EventGridRowViewModel
            {
                Id = futureEventId,
                Name = "UnitTest1",
                IsPast = false,
                StartDate = "01.01.2099 00:00"
            };

            _futureEventDetails = new EventDetailsViewModel
            {
                Id = futureEventId,
                Name = "UnitTest1",
                Address = "UnitTest1",
                Notes = "UnitTest1",
                IsPast = false,
                StartDate = "01.01.2099 00:00",
                Attendees = new List<AttendeeGridViewModel>()
            };

            _pastEvent = new Event
            {
                Id = pastEventId,
                Address = "UnitTest2",
                Name = "UnitTest2",
                Notes = "UnitTest2",
                StartDate = new DateTime(1999, 1, 1, 0, 0, 0)
            };

            _pastEventGridRow = new EventGridRowViewModel
            {
                Id = pastEventId,
                Name = "UnitTest2",
                IsPast = true,
                StartDate = "01.01.1999 00:00"
            };
        }

        [Test]
        public async Task GetEvents_returnsEventGrid()
        {
            var events = new List<Event>() { _futureEvent, _pastEvent };
            _eventRepository.GetEvents().Returns(events);

            var result = await _eventWebService.GetEvents();
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Data);

            var futureEvents = result.Data.FutureEvents;
            var pastEvents = result.Data.PastEvents;
            Assert.That(futureEvents,
                Has.Exactly(1).Matches<EventGridRowViewModel>(x =>
                    x.Id == _futureEventGridRow.Id && x.IsPast == _futureEventGridRow.IsPast && x.Name == _futureEventGridRow.Name && x.StartDate == _futureEventGridRow.StartDate));
            Assert.That(pastEvents,
                Has.Exactly(1).Matches<EventGridRowViewModel>(x =>
                    x.Id == _pastEventGridRow.Id && x.IsPast == _pastEventGridRow.IsPast && x.Name == _pastEventGridRow.Name && x.StartDate == _pastEventGridRow.StartDate));
        }

        [Test]
        public async Task GetEvent_returnsEventDetails()
        {
            var eventId = _futureEvent.Id;
            _eventRepository.GetEventById(eventId).Returns(_futureEvent);

            var result = await _eventWebService.GetEventDetails(eventId);

            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Data);
            AssertExtensions.AreEqualByJson(result.Data, _futureEventDetails);
        }

        [Test]
        public async Task GetEventByInvalidId_returnsError()
        {
            var invalidId = Guid.Parse("00000000-0000-0000-0000-000000000000");
            _eventRepository.GetEventById(invalidId).ReturnsNull();

            var result = await _eventWebService.GetEventDetails(invalidId);
            var message = $"Event with id {invalidId} was not found";

            Assert.IsFalse(result.Success);
            Assert.IsNull(result.Data);
            Assert.That(result.Messages, Has.Exactly(1).Matches<SimpleMessage>(x => x.Header == message));
        }

        [Test]
        public async Task AddEvent_returnsEventDetails()
        {
            var saveModel = new EventSaveModel
            {
                Name = _futureEvent.Name,
                Address = _futureEvent.Address,
                StartDate = "2099-01-01T00:00:00",
                Notes = _futureEvent.Notes
            };

            _eventSaveValidator.Validate(saveModel).Returns(new ValidationMessages());
            _eventRepository.AddEvent(Arg.Any<Event>()).ReturnsForAnyArgs(_futureEvent);

            var result = await _eventWebService.SaveEvent(saveModel);

            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Data);
            AssertExtensions.AreEqualByJson(result.Data, _futureEventDetails);
        }

        [Test]
        public async Task UpdateEvent_returnsEventDetails()
        {
            var saveModel = new EventSaveModel
            {
                Id = _futureEvent.Id,
                Name = _futureEvent.Name,
                Address = _futureEvent.Address,
                StartDate = "2099-01-01T00:00:00",
                Notes = _futureEvent.Notes
            };

            _eventSaveValidator.Validate(saveModel).Returns(new ValidationMessages());
            _eventRepository.UpdateEvent(Arg.Any<Event>()).ReturnsForAnyArgs(_futureEvent);

            var result = await _eventWebService.SaveEvent(saveModel);

            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Data);
            AssertExtensions.AreEqualByJson(result.Data, _futureEventDetails);
        }

        [Test]
        public async Task SaveEventInvalidData_returnsValidationErrors()
        {
            var saveModel = new EventSaveModel
            {
                Name = string.Empty,
                Address = string.Empty,
                StartDate = string.Empty,
                Notes = new string('*', 1001)
        };

            var eventSaveValidator = new EventSaveValidator();
            var result = await eventSaveValidator.Validate(saveModel);

            var messages = result.GetWebMessages().ToList();
            var expectedMessages = new[]
            {
                ErrorMessages.EventNameRequired,
                ErrorMessages.EventAddressRequired,
                ErrorMessages.EventStartTimeRequired,
                ErrorMessages.EventNotesMaxLengthExceeded
            };

            Assert.IsFalse(result.IsValid);
            Assert.IsNotEmpty(messages);
            Assert.That(messages, Has.Exactly(4).Matches<SimpleMessage>(x => expectedMessages.Any(y => x.Header == y)));
        }

        [Test]
        public async Task SaveEventInvalidStartTime_returnsValidationErrors()
        {
            var saveModel = new EventSaveModel
            {
                Name = _futureEvent.Name,
                Address = _futureEvent.Address,
                StartDate = "1000-01-01T00:00:00"
            };

            var eventSaveValidator = new EventSaveValidator();
            var result = await eventSaveValidator.Validate(saveModel);

            var messages = result.GetWebMessages().ToList();

            Assert.IsFalse(result.IsValid);
            Assert.IsNotEmpty(messages);
            Assert.That(messages, Has.Exactly(1).Matches<SimpleMessage>(x => x.Header == ErrorMessages.EventStartTimeInPast));
        }

        [Test]
        public async Task DeleteEvent_returnsSuccess()
        {
            var eventId = _futureEvent.Id;
            var result = await _eventWebService.DeleteEvent(eventId);

            Assert.IsTrue(result.Success);
        }

        [Test]
        public async Task DeleteEventByInvalidId_returnsError()
        {
            var invalidId = Guid.Parse("00000000-0000-0000-0000-000000000000");
            var message = $"Event with id {invalidId} does not exist";

            _eventRepository.DeleteEvent(invalidId).Throws(new SimpleException(message));

            var result = await _eventWebService.DeleteEvent(invalidId);

            Assert.IsFalse(result.Success);
            Assert.That(result.Messages, Has.Exactly(1).Matches<SimpleMessage>(x => x.Header == message));
        }
    }
}