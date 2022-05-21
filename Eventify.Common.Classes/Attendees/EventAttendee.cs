using System;
using Eventify.Common.Classes.Events;

namespace Eventify.Common.Classes.Attendees
{
	public class EventAttendee
	{
		public Guid Id { get; set; }

		public Guid EventId { get; set; }

		public Guid AttendeeId { get; set; }

		public DateTime CreatedDate { get; set; }

		public string PaymentMethod { get; set; }

		public int? Participants { get; set; }

		public string? Notes { get; set; }

		public Event Event { get; set; }

		public Attendee Attendee { get; set; }
	}
}
