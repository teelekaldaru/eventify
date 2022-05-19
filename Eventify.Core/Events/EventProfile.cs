using System;
using AutoMapper;
using Eventify.Common.Classes.Events;

namespace Eventify.Core.Events
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Event, EventGridRowViewModel>()
                .ForMember(d => d.IsPast, o => o.Ignore());

            CreateMap<Event, EventDetailsViewModel>()
                .ForMember(d => d.Attendees, o => o.Ignore())
                .ForMember(d => d.IsPast, o => o.Ignore());

            CreateMap<EventSaveModel, Event>()
	            .ForMember(d => d.StartDate, o => o.MapFrom(s => DateTime.Parse(s.StartDate)));
        }
    }
}
