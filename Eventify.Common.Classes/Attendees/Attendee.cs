using System;
using Eventify.Common.Utils.Exceptions;

namespace Eventify.Common.Classes.Attendees
{
    public class Attendee
    {
	    public Guid Id { get; set; }

	    public string Name { get; set; }

	    public string Code { get; set; }

	    public AttendeeType AttendeeType { get; set; }

	    public (string, string) GetFirstLastName()
	    {
		    var parts = Name.Split(" ");
		    if (parts.Length < 2 && AttendeeType == AttendeeType.Person)
		    {
			    throw new SimpleException("First or last name is missing");
		    }

		    return (string.Join(" ", parts[..^1]), parts[^1]);
	    }
    }
}
