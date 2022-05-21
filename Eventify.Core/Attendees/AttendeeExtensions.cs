using Eventify.Common.Classes.Attendees;
using Eventify.Common.Classes.AutoMapper;
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
            return MapperWrapper.Mapper.Map<AttendeeViewModel>(eventAttendee);
        }

        public static EventAttendee ToEventAttendee(this AttendeeSaveModel saveModel)
        {
	        var eventAttendee = MapperWrapper.Mapper.Map<EventAttendee>(saveModel);
            eventAttendee.Attendee = saveModel.ToAttendee();
            return eventAttendee;
        }

        public static EventAttendeeUpdateSet ToUpdateSet(this AttendeeSaveModel saveModel)
        {
	        return MapperWrapper.Mapper.Map<EventAttendeeUpdateSet>(saveModel);
        }

        private static Attendee ToAttendee(this AttendeeSaveModel saveModel)
        {
	        return MapperWrapper.Mapper.Map<Attendee>(saveModel);
        }
    }
}
