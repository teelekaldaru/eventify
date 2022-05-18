using Eventify.Core.Base.Services;
using Eventify.DAL.Events;
using System;
using System.Threading.Tasks;
using Eventify.Common.Utils.Logger;
using Eventify.Common.Utils.Messages.RequestResult;

namespace Eventify.Core.Events
{
	public interface IEventWebService
    {
        Task<RequestResult<EventGridViewModel>> GetEvents();

        Task<RequestResult<EventDetailsViewModel>> GetEventDetails(Guid eventId);

        Task<RequestResult<EventDetailsViewModel>> SaveEvent(EventSaveModel saveModel);

        Task<RequestResult> DeleteEvent(Guid eventId);
    }

    internal class EventWebService : BaseWebService, IEventWebService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IEventSaveValidator _eventSaveValidator;

        public EventWebService(
            ILogger logger,
            IEventRepository eventRepository,
            IEventSaveValidator eventSaveValidator) : base(logger)
        {
	        _eventRepository = eventRepository;
	        _eventSaveValidator = eventSaveValidator;
        }

        public async Task<RequestResult<EventGridViewModel>> GetEvents()
        {
            try
            {
                var events = await _eventRepository.GetEvents();
                var result = events.ToGridViewModel();
                return RequestResult<EventGridViewModel>.CreateSuccess(result);
            }
            catch (Exception e)
            {
                return HandleException<EventGridViewModel>(e);
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
	            var validationResult = await _eventSaveValidator.Validate(saveModel);
	            if (!validationResult.IsValid)
	            {
		            return RequestResult<EventDetailsViewModel>.CreateValidation(validationResult);

	            }

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
