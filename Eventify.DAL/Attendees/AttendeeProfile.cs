using AutoMapper;
using Eventify.Common.Classes.Attendees;
using Eventify.Domain;

namespace Eventify.DAL.Attendees
{
    public class AttendeeProfile : Profile
    {
        public AttendeeProfile()
        {
            CreateMap<DbAttendee, Attendee>().ReverseMap();
            CreateMap<DbPerson, Person>().ReverseMap();
            CreateMap<DbCompany, Company>().ReverseMap();
        }
    }
}
