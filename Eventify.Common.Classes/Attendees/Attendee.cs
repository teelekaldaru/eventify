using System;

namespace Eventify.Common.Classes.Attendees
{
    public class Attendee
    {
	    public Guid Id { get; set; }

	    public string Name { get; set; }

        public string LastName { get; set; }

        public string Code { get; set; }

	    public AttendeeType AttendeeType { get; set; }

	    public string GetFullName()
        {
            return string.Join(" ", Name, LastName);
        }
    }
}
