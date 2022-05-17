using System;
using System.Threading.Tasks;
using Eventify.Common.Classes.Logger;
using Eventify.Common.Classes.Messages.RequestResult;
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

        public AttendeeWebService(
            ILogger logger,
            IAttendeeRepository attendeeRepository) : base(logger)
        {
            _attendeeRepository = attendeeRepository;
        }

        public async Task<RequestResult<AttendeeDetailsViewModel>> GetAttendeeDetails(Guid attendeeId)
        {
            try
            {
                var attendee = await _attendeeRepository.GetAttendeeById(attendeeId);
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
                throw new NotImplementedException();
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
                throw new NotImplementedException();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
