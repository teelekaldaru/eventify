using System;
using AutoMapper;
using Eventify.Common.Classes.Attendees;
using Eventify.DAL.Attendees;

namespace Eventify.Core.Attendees
{
    public class AttendeeProfile : Profile
    {
        public AttendeeProfile()
        {
            CreateMap<AttendeeSaveModel, EventAttendee>()
                .ForMember(d => d.CreatedDate, o => o.MapFrom(s => DateTime.Now))
                .ForMember(d => d.Attendee, o => o.Ignore())
                .ForMember(d => d.Event, o => o.Ignore());

            CreateMap<AttendeeSaveModel, Attendee>();

            CreateMap<AttendeeSaveModel, EventAttendeeUpdateSet>()
                .ForMember(d => d.AttendeeCode, o => o.MapFrom(s => s.Code))
                .ForMember(d => d.AttendeeName, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.AttendeeLastName, o => o.MapFrom(s => s.LastName));

            CreateMap<EventAttendee, AttendeeGridViewModel>()
                .ForMember(d => d.FullName, o => o.MapFrom(s => s.Attendee.GetFullName()))
                .ForMember(d => d.Code, o => o.MapFrom(s => s.Attendee.Code));

            CreateMap<EventAttendee, AttendeeViewModel>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Attendee.Name))
                .ForMember(d => d.LastName, o => o.MapFrom(s => s.Attendee.LastName))
                .ForMember(d => d.Code, o => o.MapFrom(s => s.Attendee.Code))
                .ForMember(d => d.AttendeeType, o => o.MapFrom(s => s.Attendee.AttendeeType));
        }
    }
}
