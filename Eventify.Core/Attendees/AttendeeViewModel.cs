using System;
using Eventify.Common.Classes.Attendees;

namespace Eventify.Core.Attendees
{
    public class AttendeeGridViewModel
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string Code { get; set; }
    }

    public class AttendeeViewModel
    {
	    public Guid Id { get; set; }

        public Guid EventId { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Code { get; set; }

        public string PaymentMethod { get; set; }

        public string Notes { get; set; }

        public int? Participants { get; set; }

        public AttendeeType AttendeeType { get; set; }
    }
}
