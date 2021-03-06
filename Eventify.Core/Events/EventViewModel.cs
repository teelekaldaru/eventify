using System;
using System.Collections.Generic;
using Eventify.Core.Attendees;

namespace Eventify.Core.Events
{
    public class EventGridViewModel
    {
        public IEnumerable<EventGridRowViewModel> PastEvents { get; set; }

        public IEnumerable<EventGridRowViewModel> FutureEvents { get; set; }
    }

    public class EventGridRowViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string StartDate { get; set; }

        public bool IsPast { get; set; }
    }

    public class EventDetailsViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string StartDate { get; set; }

        public bool IsPast { get; set; }

        public string? Notes { get; set; }

        public IEnumerable<AttendeeGridViewModel> Attendees { get; set; }
    }
}
