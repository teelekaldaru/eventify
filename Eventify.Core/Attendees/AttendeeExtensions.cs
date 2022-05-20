using Eventify.Common.Classes.Attendees;
using Eventify.Common.Classes.AutoMapper;
using Eventify.Common.Utils.Exceptions;
using Eventify.DAL.Attendees;

namespace Eventify.Core.Attendees
{
    internal static class AttendeeExtensions
    {
        public static AttendeeGridViewModel ToGridViewModel(this EventAttendee eventAttendee)
        {
	        return MapperWrapper.Mapper.Map<AttendeeGridViewModel>(eventAttendee);
        }

        public static AttendeeViewModel ToViewModel(this EventAttendee eventAttendee)
        {
	        var attendeeType = eventAttendee.Attendee.AttendeeType;

			return attendeeType switch
			{
				AttendeeType.Person => eventAttendee.ToPersonViewModel(),
				AttendeeType.Company => eventAttendee.ToCompanyViewModel(),
				_ => throw new SimpleException($"Invalid attendee type: {attendeeType}")
			};
		}

        public static EventAttendee ToEventAttendee(this AttendeeSaveModel saveModel)
        {
	        return MapperWrapper.Mapper.Map<EventAttendee>(saveModel);
        }

        public static EventAttendeeUpdateSet ToUpdateSet(this AttendeeSaveModel saveModel)
        {
	        return MapperWrapper.Mapper.Map<EventAttendeeUpdateSet>(saveModel);
        }

        private static AttendeePersonViewModel ToPersonViewModel(this EventAttendee eventAttendee)
        {
	        return MapperWrapper.Mapper.Map<AttendeePersonViewModel>(eventAttendee);
        }

        private static AttendeeCompanyViewModel ToCompanyViewModel(this EventAttendee eventAttendee)
        {
	        return MapperWrapper.Mapper.Map<AttendeeCompanyViewModel>(eventAttendee);
        }
    }
}
