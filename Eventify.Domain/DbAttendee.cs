using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eventify.Common.Classes.Attendees;

namespace Eventify.Domain
{
    [Table("Attendee")]
    public class DbAttendee
    {
        [Key]
        public Guid Id { get; set; }

        public Guid EventId { get; set; }

        public Guid? PersonId { get; set; }
        
        public Guid? CompanyId { get; set; }

        public DateTime CreatedDate { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public string AdditionalInfo { get; set; }

        public virtual DbEvent Event { get; set; }

        public virtual DbPerson Person { get; set; }

        public virtual DbCompany Company { get; set; }
    }
}
