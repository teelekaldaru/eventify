using Eventify.Common.Classes.Attendees;
using Eventify.Common.Classes.AutoMapper;
using Eventify.Domain;

namespace Eventify.DAL.Attendees
{
    internal static class AttendeeExtensions
    {
        public static EventAttendee ToEventAttendee(this DbEventAttendee dbEventAttendee)
        {
            return MapperWrapper.Mapper.Map<EventAttendee>(dbEventAttendee);
        }

        public static DbEventAttendee ToDbEventAttendee(this EventAttendee eventAttendee)
        {
            return MapperWrapper.Mapper.Map<DbEventAttendee>(eventAttendee);
        }

        public static Attendee ToAttendee(this DbAttendee dbAttendee)
        {
	        return MapperWrapper.Mapper.Map<Attendee>(dbAttendee);
        }

        public static DbAttendee ToDbAttendee(this Attendee attendee)
        {
	        return MapperWrapper.Mapper.Map<DbAttendee>(attendee);
        }

        public static void ApplyUpdateSet(this DbEventAttendee? entity, EventAttendeeUpdateSet? updateSet)
        {
	        if (entity == null || updateSet == null)
	        {
		        return;
	        }

	        if (!string.IsNullOrWhiteSpace(updateSet.AttendeeCode))
	        {
		        entity.Attendee.Code = updateSet.AttendeeCode;
	        }

	        if (!string.IsNullOrWhiteSpace(updateSet.AttendeeName))
	        {
		        entity.Attendee.Name = updateSet.AttendeeName;
	        }

            if (!string.IsNullOrWhiteSpace(updateSet.PaymentMethod))
	        {
		        entity.PaymentMethod = updateSet.PaymentMethod;
	        }

	        if (updateSet.Participants.HasValue)
	        {
		        entity.Participants = updateSet.Participants;
	        }

	        entity.Notes = updateSet.Notes;
        }
    }
}
