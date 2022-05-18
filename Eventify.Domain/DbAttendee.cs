using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public int? Participants { get; set; }

        public string PaymentMethod { get; set; }

        public string AdditionalInformation { get; set; }

        public virtual DbEvent Event { get; set; }

        public virtual DbPerson Person { get; set; }

        public virtual DbCompany Company { get; set; }
    }
}
