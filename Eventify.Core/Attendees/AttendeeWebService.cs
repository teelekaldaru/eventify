using System;
using System.Threading.Tasks;
using Eventify.Common.Utils.Logger;
using Eventify.Common.Utils.Messages.RequestResult;
using Eventify.Core.Base.Services;
using Eventify.DAL.Attendees;

namespace Eventify.Core.Attendees
{
	public interface IAttendeeWebService
    {
        Task<RequestResult<AttendeeDetailsViewModel>> GetAttendeeDetails(Guid attendeeId);

        Task<RequestResult<AttendeeGridViewModel>> SaveAttendee(AttendeeSaveModel saveModel);

        Task<RequestResult> DeleteAttendee(Guid attendeeId);
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

        public async Task<RequestResult<AttendeeDetailsViewModel>> GetAttendeeDetails(Guid attendeeId)
        {
            try
            {
                var attendee = await _attendeeRepository.GetAttendeeById(attendeeId);
                return RequestResult<AttendeeDetailsViewModel>.CreateSuccess(new AttendeeDetailsViewModel());
            }
            catch (Exception e)
            {
                return HandleException<AttendeeDetailsViewModel>(e);
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

	            return RequestResult<AttendeeGridViewModel>.CreateSuccess(new AttendeeGridViewModel());
            }
            catch (Exception e)
            {
                return HandleException<AttendeeGridViewModel>(e);
            }
        }

        public async Task<RequestResult> DeleteAttendee(Guid attendeeId)
        {
            try
            {
	            await _attendeeRepository.DeleteAttendee(attendeeId);
	            return RequestResult.CreateSuccess();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
