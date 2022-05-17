using Eventify.Common.Classes.Attendees;
using Eventify.Common.Classes.AutoMapper;
using Eventify.Domain;

namespace Eventify.DAL.Attendees
{
    internal static class AttendeeExtensions
    {
        public static Attendee ToAttendee(this DbAttendee dbAttendee)
        {
            return MapperWrapper.Mapper.Map<Attendee>(dbAttendee);
        }

        public static DbAttendee ToDbAttendee(this Attendee attendee)
        {
            return MapperWrapper.Mapper.Map<DbAttendee>(attendee);
        }

        public static Person ToPerson(this DbPerson dbPerson)
        {
            return MapperWrapper.Mapper.Map<Person>(dbPerson);
        }

        public static DbPerson ToDbPerson(this Person person)
        {
            return MapperWrapper.Mapper.Map<DbPerson>(person);
        }

        public static Company ToCompany(this DbCompany dbCompany)
        {
            return MapperWrapper.Mapper.Map<Company>(dbCompany);
        }

        public static DbCompany ToDbCompany(this Company company)
        {
            return MapperWrapper.Mapper.Map<DbCompany>(company);
        }
    }
}
