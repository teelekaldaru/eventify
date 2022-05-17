
using System.Linq;
using Eventify.Common.Classes.AutoMapper;
using Eventify.Common.Classes.Events;
using Eventify.Core.Attendees;

namespace Eventify.Core.Events
{
    internal static class EventExtensions
    {
        public static EventGridViewModel ToGridViewModel(this Event entity)
        {
            return MapperWrapper.Mapper.Map<EventGridViewModel>(entity);
        }

        public static EventDetailsViewModel ToViewModel(this Event entity)
        {
            var viewModel = MapperWrapper.Mapper.Map<EventDetailsViewModel>(entity);
            viewModel.Attendees = entity.Attendees.Select(x => x.ToGridViewModel());
            return viewModel;
        }

        public static Event ToEvent(this EventSaveModel saveModel)
        {
            return MapperWrapper.Mapper.Map<Event>(saveModel);
        }
    }
}
