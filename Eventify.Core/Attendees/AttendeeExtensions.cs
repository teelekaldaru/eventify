using Eventify.Common.Classes.Attendees;

namespace Eventify.Core.Attendees
{
    internal static class AttendeeExtensions
    {
        public static AttendeeGridViewModel ToGridViewModel(this Attendee attendee)
        {
            return new AttendeeGridViewModel();
        }
    }
}
