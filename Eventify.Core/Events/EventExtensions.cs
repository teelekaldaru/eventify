using System;
using System.Collections.Generic;
using System.Linq;
using Eventify.Common.Classes.AutoMapper;
using Eventify.Common.Classes.Events;
using Eventify.Core.Attendees;

namespace Eventify.Core.Events
{
    internal static class EventExtensions
    {
        public static EventGridViewModel ToGridViewModel(this IEnumerable<Event> events)
        {
            var rows = events.Select(e => e.ToGridRowViewModel()).ToList();
            return new EventGridViewModel
            {
                FutureEvents = rows.Where(x => !x.IsPast),
                PastEvents = rows.Where(x => x.IsPast)
            };
        }

        public static EventGridRowViewModel ToGridRowViewModel(this Event entity)
        {
            return MapperWrapper.Mapper.Map<EventGridRowViewModel>(entity);
        }

        public static EventDetailsViewModel ToViewModel(this Event entity)
        {
            var viewModel = MapperWrapper.Mapper.Map<EventDetailsViewModel>(entity);
            viewModel.Attendees = entity.EventAttendees
                .OrderBy(x => x.CreatedDate)
                .Select(x => x.ToGridViewModel());
            return viewModel;
        }

        public static Event ToEvent(this EventSaveModel saveModel)
        {
            return MapperWrapper.Mapper.Map<Event>(saveModel);
        }
    }
}
