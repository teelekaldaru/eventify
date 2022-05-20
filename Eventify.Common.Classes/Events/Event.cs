using System;
using System.Collections.Generic;
using Eventify.Common.Classes.Attendees;

namespace Eventify.Common.Classes.Events
{
    public class Event
    {
	    public Guid Id { get; set; }

	    public string Name { get; set; }

	    public string Address { get; set; }

	    public DateTime StartDate { get; set; }

	    public string? Notes { get; set; }

	    public IEnumerable<EventAttendee> EventAttendees { get; set; }
    }
}
