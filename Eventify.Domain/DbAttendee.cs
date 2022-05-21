using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eventify.Common.Classes.Attendees;
using Eventify.Common.Utils.Database;

namespace Eventify.Domain
{
    [Table("Attendee")]
    public class DbAttendee : IHasGuidId
    {
	    [Key]
	    public Guid Id { get; set; }

        public string Name { get; set; }

        public string? LastName { get; set; }

        public string Code { get; set; }

        public AttendeeType AttendeeType { get; set; }

        public virtual ICollection<DbEventAttendee> AttendedEvents { get; set; }
    }
}
