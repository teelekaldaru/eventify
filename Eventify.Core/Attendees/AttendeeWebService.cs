using System;
using System.Threading.Tasks;
using Eventify.Common.Classes.Attendees;
using Eventify.Common.Utils.Logger;
using Eventify.Common.Utils.Messages.RequestResult;
using Eventify.Core.Base.Services;
using Eventify.DAL.Attendees;

namespace Eventify.Core.Attendees
{
	public interface IAttendeeWebService
    {
        Task<RequestResult<AttendeeViewModel>> GetEventAttendee(Guid attendeeId);

        Task<RequestResult<AttendeeGridViewModel>> SaveAttendee(AttendeeSaveModel saveModel);

        Task<RequestResult> RemoveAttendeeFromEvent(Guid eventAttendeeId);
    }

    internal class AttendeeWebService : BaseWebService, IAttendeeWebService
    {
        private readonly IAttendeeRepository _attendeeRepository;
        private readonly IAttendeeSaveValidator _attendeeSaveValidator;

        public AttendeeWebService(
            ILogger logger,
            IAttendeeRepository attendeeRepository,
            IAttendeeSaveValidator attendeeSaveValidator) : base(logger)
        {
	        _attendeeRepository = attendeeRepository;
	        _attendeeSaveValidator = attendeeSaveValidator;
        }

        public async Task<RequestResult<AttendeeViewModel>> GetEventAttendee(Guid attendeeId)
        {
            try
            {
                var attendee = await _attendeeRepository.GetEventAttendeeById(attendeeId);
                var result = attendee.ToViewModel();
                return RequestResult<AttendeeViewModel>.CreateSuccess(result);
            }
            catch (Exception e)
            {
                return HandleException<AttendeeViewModel>(e);
            }
        }

        public async Task<RequestResult<AttendeeGridViewModel>> SaveAttendee(AttendeeSaveModel saveModel)
        {
            try
            {
	            var validationResult = await _attendeeSaveValidator.Validate(saveModel);
	            if (!validationResult.IsValid)
	            {
		            return RequestResult<AttendeeGridViewModel>.CreateValidation(validationResult);
	            }

	            EventAttendee eventAttendee;
	            if (saveModel.Id.HasValue)
	            {
		            var updateSet = saveModel.ToUpdateSet();
		            eventAttendee = await _attendeeRepository.UpdateEventAttendee(saveModel.Id.Value, updateSet);
	            }
	            else
	            {
		            var attendeeToAdd = saveModel.ToEventAttendee();
		            eventAttendee = await _attendeeRepository.AddEventAttendee(attendeeToAdd);
	            }

	            var result = eventAttendee.ToGridViewModel();
                return RequestResult<AttendeeGridViewModel>.CreateSuccess(result);
            }
            catch (Exception e)
            {
                return HandleException<AttendeeGridViewModel>(e);
            }
        }

        public async Task<RequestResult> RemoveAttendeeFromEvent(Guid eventAttendeeId)
        {
            try
            {
	            await _attendeeRepository.DeleteEventAttendee(eventAttendeeId);
	            return RequestResult.CreateSuccess();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
