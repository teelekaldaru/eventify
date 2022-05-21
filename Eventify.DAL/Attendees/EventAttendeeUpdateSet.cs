namespace Eventify.DAL.Attendees
{
	public class EventAttendeeUpdateSet
	{
		public string AttendeeName { get; set; }

		public string AttendeeLastName { get; set; }

		public string AttendeeCode { get; set; }
		
		public string PaymentMethod { get; set; }

		public string Notes { get; set; }

		public int? Participants { get; set; }
	}
}
