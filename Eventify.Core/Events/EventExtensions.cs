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
            var row = MapperWrapper.Mapper.Map<EventGridRowViewModel>(entity);
            row.IsPast = entity.StartDate < DateTime.Now;
            row.StartDate = entity.StartDate.ToString("dd.MM.yyyy HH:mm");
            return row;
        }

        public static EventDetailsViewModel ToViewModel(this Event entity)
        {
            var viewModel = MapperWrapper.Mapper.Map<EventDetailsViewModel>(entity);
            viewModel.IsPast = entity.StartDate < DateTime.Now;
            viewModel.StartDate = entity.StartDate.ToString("dd.MM.yyyy HH:mm");
            viewModel.Attendees = entity.EventAttendees.Select(x => x.ToGridViewModel());
            return viewModel;
        }

        public static Event ToEvent(this EventSaveModel saveModel)
        {
            return MapperWrapper.Mapper.Map<Event>(saveModel);
        }
    }
}
