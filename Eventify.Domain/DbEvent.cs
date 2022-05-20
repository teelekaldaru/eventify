using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eventify.Common.Utils.Database;

namespace Eventify.Domain
{
    [Table("Event")]
    public class DbEvent : IHasGuidId
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public DateTime StartDate { get; set; }

        public string? Notes { get; set; }

        public virtual ICollection<DbEventAttendee> EventAttendees { get; set; }
    }
}
