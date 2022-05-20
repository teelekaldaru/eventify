using System;
using Eventify.Common.Classes.Attendees;

namespace Eventify.Core.Attendees
{
	public class AttendeeSaveModel
    {
        public Guid? Id { get; set; }

        public string Name { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PersonalCode { get; set; }

        public string RegisterCode { get; set; }

        public string PaymentMethod { get; set; }

        public string Notes { get; set; }

        public AttendeeType? AttendeeType { get; set; }
    }
}
