using AutoMapper;
using Eventify.Common.Classes.Events;
using Eventify.Domain;

namespace Eventify.DAL.Events
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<DbEvent, Event>().ReverseMap();
        }
    }
}
