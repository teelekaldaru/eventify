using System;

namespace Eventify.Common.Classes.Attendees
{
    public class Person
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PersonalCode { get; set; }

        public string GetFullName()
        {
            return FirstName + " " + LastName;
        }
    }
}
