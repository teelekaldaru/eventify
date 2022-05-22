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

        [MaxLength(256)]
        public string Name { get; set; }

        [MaxLength(512)]
        public string Address { get; set; }

        public DateTime StartDate { get; set; }

        [MaxLength(1000)]
        public string? Notes { get; set; }

        public virtual ICollection<DbEventAttendee> EventAttendees { get; set; }
    }
}
