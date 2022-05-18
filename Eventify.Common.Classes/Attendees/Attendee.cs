using System;
using Eventify.Common.Classes.Events;

namespace Eventify.Common.Classes.Attendees
{
    public class Attendee
    {
        public Guid Id { get; set; }

        public Guid EventId { get; set; }

        public Guid? PersonId { get; set; }

        public Guid? CompanyId { get; set; }

        public DateTime CreatedDate { get; set; }

        public string PaymentMethod { get; set; }

        public string AdditionalInfo { get; set; }

        public Event Event { get; set; }

        public Person? Person { get; set; }

        public Company? Company { get; set; }
    }
}
