using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eventify.Common.Utils.Database;

namespace Eventify.Domain
{
	[Table("EventAttendee")]
	public class DbEventAttendee : IHasGuidId
	{
		[Key]
		public Guid Id { get; set; }

		public Guid EventId { get; set; }

		public Guid AttendeeId { get; set; }

		public DateTime CreatedDate { get; set; }

		public string PaymentMethod { get; set; }

		public int? Participants { get; set; }

		public string? Notes { get; set; }

		public virtual DbEvent Event { get; set; }

		public virtual DbAttendee Attendee { get; set; }
	}
}
