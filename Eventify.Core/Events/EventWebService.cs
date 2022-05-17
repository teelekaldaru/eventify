using Eventify.Common.Classes.Logger;
using Eventify.Common.Classes.Messages.RequestResult;
using Eventify.Core.Base.Services;
using Eventify.DAL.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventify.Core.Events
{
    public interface IEventWebService
    {
        Task<RequestResult<IEnumerable<EventGridViewModel>>> GetEvents();

        Task<RequestResult<EventDetailsViewModel>> GetEventDetails(Guid eventId);

        Task<RequestResult<EventDetailsViewModel>> SaveEvent(EventSaveModel saveModel);

        Task<RequestResult> DeleteEvent(Guid eventId);
    }

    internal class EventWebService : BaseWebService, IEventWebService
    {
        private readonly IEventRepository _eventRepository;

        public EventWebService(
            ILogger logger,
            IEventRepository eventRepository) : base(logger)
        {
            _eventRepository = eventRepository;
        }

        public async Task<RequestResult<IEnumerable<EventGridViewModel>>> GetEvents()
        {
            try
            {
                var events = await _eventRepository.GetEvents();
                var viewModels = events.Select(x => x.ToGridViewModel());
                return RequestResult<IEnumerable<EventGridViewModel>>.CreateSuccess(viewModels);
            }
            catch (Exception e)
            {
                return HandleException<IEnumerable<EventGridViewModel>>(e);
            }
        }

        public async Task<RequestResult<EventDetailsViewModel>> GetEventDetails(Guid eventId)
        {
            try
            {
                var eventEntity = await _eventRepository.GetEventById(eventId);
                return eventEntity == null
                    ? RequestResult<EventDetailsViewModel>.CreateError($"Event with id {eventId} was not found")
                    : RequestResult<EventDetailsViewModel>.CreateSuccess(new EventDetailsViewModel());
            }
            catch (Exception e)
            {
                return HandleException<EventDetailsViewModel>(e);
            }
        }

        public async Task<RequestResult<EventDetailsViewModel>> SaveEvent(EventSaveModel saveModel)
        {
            try
            {
                var eventToSave = saveModel.ToEvent();
                var savedEvent = !saveModel.Id.HasValue
                    ? await _eventRepository.AddEvent(eventToSave)
                    : await _eventRepository.UpdateEvent(eventToSave);

                var viewModel = savedEvent.ToViewModel();
                return RequestResult<EventDetailsViewModel>.CreateSuccess(viewModel);
            }
            catch (Exception e)
            {
                return HandleException<EventDetailsViewModel>(e);
            }
        }

        public async Task<RequestResult> DeleteEvent(Guid eventId)
        {
            try
            {
                await _eventRepository.DeleteEvent(eventId);
                return RequestResult.CreateSuccess();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
