using System;
using System.Collections.Generic;
using Eventify.Core.Attendees;

namespace Eventify.Core.Events
{
    public class EventGridViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public bool IsPast { get; set; }
    }

    public class EventDetailsViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public DateTime StartDate { get; set; }

        public string Description { get; set; }

        public IEnumerable<AttendeeGridViewModel> Attendees { get; set; }
    }
}
