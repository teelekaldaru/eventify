using System.Collections;
using System.Reflection;
using Eventify.Common.Classes.Attendees;
using Eventify.Common.Classes.Events;
using Eventify.Common.Utils.Exceptions;
using Eventify.Common.Utils.Messages;
using Eventify.Common.Utils.Validations;
using Eventify.Core.Attendees;
using Eventify.Core.Events;
using Eventify.DAL.Events;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using ILogger = Eventify.Common.Utils.Logger.ILogger;

namespace Eventify.UnitTests
{
    public class EventWebServiceTests : UnitTest
    {
        private IEventWebService _eventWebService;

        private Mock<ILogger> _logger;
        private Mock<IEventRepository> _eventRepository;
        private Mock<IEventSaveValidator> _eventSaveValidator;

        private Event _futureEvent;
        private Event _pastEvent;

        private EventGridRowViewModel _futureEventGridRow;
        private EventGridRowViewModel _pastEventGridRow;

        private EventDetailsViewModel _futureEventDetails;

        [SetUp]
        public void Setup()
        {
            _logger = new Mock<ILogger>();
            _eventRepository = new Mock<IEventRepository>();
            _eventSaveValidator = new Mock<IEventSaveValidator>();

            _eventWebService = new EventWebService(_logger.Object, _eventRepository.Object, _eventSaveValidator.Object);

            _futureEvent = new Event
            {
                Id = Guid.Parse("b26dedff-c7e0-4847-8d7d-dbcf50d09546"),
                Address = "UnitTest1",
                Name = "UnitTest1",
                Notes = "UnitTest1",
                StartDate = new DateTime(2099, 1, 1, 0, 0, 0),
                EventAttendees = new List<EventAttendee>()
            };

            _futureEventGridRow = new EventGridRowViewModel
            {
                Id = Guid.Parse("b26dedff-c7e0-4847-8d7d-dbcf50d09546"),
                Name = "UnitTest1",
                IsPast = false,
                StartDate = "01.01.2099 00:00"
            };

            _futureEventDetails = new EventDetailsViewModel
            {
                Id = Guid.Parse("b26dedff-c7e0-4847-8d7d-dbcf50d09546"),
                Name = "UnitTest1",
                Address = "UnitTest1",
                Notes = "UnitTest1",
                IsPast = false,
                StartDate = "01.01.2099 00:00",
                Attendees = new List<AttendeeGridViewModel>()
            };

            _pastEvent = new Event
            {
                Id = Guid.Parse("1a298e25-1c40-449d-bb48-d0aeec0ddcf3"),
                Address = "UnitTest2",
                Name = "UnitTest2",
                Notes = "UnitTest2",
                StartDate = new DateTime(1999, 1, 1, 0, 0, 0)
            };

            _pastEventGridRow = new EventGridRowViewModel
            {
                Id = Guid.Parse("1a298e25-1c40-449d-bb48-d0aeec0ddcf3"),
                Name = "UnitTest2",
                IsPast = true,
                StartDate = "01.01.1999 00:00"
            };
        }

        [Test]
        public async Task GetEvents_returnsEventGrid()
        {
            var events = new List<Event>() { _futureEvent, _pastEvent };
            _eventRepository.Setup(x => x.GetEvents()).ReturnsAsync(events);

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
            _eventRepository.Setup(x => x.GetEventById(eventId)).ReturnsAsync(_futureEvent);

            var result = await _eventWebService.GetEventDetails(eventId);

            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Data);
            AssertExtensions.AreEqualByJson(result.Data, _futureEventDetails);
        }

        [Test]
        public async Task GetEventByInvalidId_returnsError()
        {
            var invalidId = Guid.Parse("00000000-0000-0000-0000-000000000000");
            _eventRepository.Setup(x => x.GetEventById(invalidId)).ReturnsAsync((Event)null);

            var result = await _eventWebService.GetEventDetails(invalidId);

            Assert.IsFalse(result.Success);
            Assert.IsNull(result.Data);
            Assert.That(result.Messages, Has.Exactly(1).Matches<SimpleMessage>(x => x.Header == $"Event with id {invalidId} was not found"));
        }

        [Test]
        public async Task AddEvent_returnsEventDetails()
        {
            var saveModel = new EventSaveModel
            {
                Name = "UnitTest1",
                Address = "UnitTest1",
                StartDate = new DateTime(2099, 1, 1, 0, 0, 0).ToString(),
                Notes = "UnitTest1"
            };

            _eventRepository.Setup(x => x.AddEvent(_futureEvent)).ReturnsAsync(_futureEvent);
            _eventSaveValidator.Setup(x => x.Validate(saveModel)).ReturnsAsync(new ValidationMessages());

            var result = await _eventWebService.SaveEvent(saveModel);

            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Data);
            AssertExtensions.AreEqualByJson(result.Data, _futureEventDetails);
        }

        /*[Test]
        public async Task UpdateEvent_returnsEventDetails()
        {
            var eventId = _futureEvent.Id;
            _eventRepository.Setup(x => x.GetEventById(eventId)).ReturnsAsync(_futureEvent);

            var result = await _eventWebService.SaveEvent(eventId);

            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Data);
            AssertExtensions.AreEqualByJson(result.Data, _futureEventDetails);
        }

        [Test]
        public async Task SaveInvalidEvent_returnsValidationErrors()
        {
            var eventId = _futureEvent.Id;
            _eventRepository.Setup(x => x.GetEventById(eventId)).ReturnsAsync(_futureEvent);

            var result = await _eventWebService.SaveEvent(eventId);

            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Data);
            AssertExtensions.AreEqualByJson(result.Data, _futureEventDetails);
        }*/

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

            _eventRepository.Setup(x => x.DeleteEvent(invalidId)).Throws(new SimpleException(message));

            var result = await _eventWebService.DeleteEvent(invalidId);

            Assert.IsFalse(result.Success);
            Assert.That(result.Messages, Has.Exactly(1).Matches<SimpleMessage>(x => x.Header == message));
        }
    }

    public static class AssertExtensions
    {
        public static void AreEqualByJson(object expected, object actual)
        {
            var expectedJson = JsonConvert.SerializeObject(expected);
            var actualJson = JsonConvert.SerializeObject(actual);
            Assert.That(actualJson, Is.EqualTo(expectedJson));
        }
    }
}