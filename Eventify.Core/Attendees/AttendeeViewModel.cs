using System;
using Eventify.Common.Classes.Attendees;

namespace Eventify.Core.Attendees
{
    public class AttendeeGridViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }
    }

    public class AttendeeViewModel
    {
	    public Guid Id { get; set; }

	    public AttendeeType AttendeeType { get; set; }
    }

    public class AttendeePersonViewModel : AttendeeViewModel
    {
	    public string FirstName { get; set; }

	    public string LastName { get; set; }

	    public string PersonalCode { get; set; }
    }

    public class AttendeeCompanyViewModel : AttendeeViewModel
    {
	    public string Name { get; set; }

        public string RegisterCode { get; set; }
    }
}
