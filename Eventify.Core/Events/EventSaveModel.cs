using System;

namespace Eventify.Core.Events
{
    public class EventSaveModel
    {
        public Guid? Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string StartDate { get; set; }

        public string? Notes { get; set; }
    }
}
