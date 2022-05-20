using System;
using AutoMapper;
using Eventify.Common.Classes.Attendees;
using Eventify.Domain;

namespace Eventify.DAL.Attendees
{
    public class AttendeeProfile : Profile
    {
        public AttendeeProfile()
        {
	        CreateMap<DbEventAttendee, EventAttendee>();
	        CreateMap<EventAttendee, DbEventAttendee>()
		        .ForMember(d => d.CreatedDate, o => o.MapFrom(s => DateTime.Now));

	        CreateMap<DbAttendee, Attendee>().ReverseMap();
        }
    }
}
