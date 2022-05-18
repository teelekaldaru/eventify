using System;

namespace Eventify.Core.Attendees
{
	public class AttendeeSaveModel
    {
        public Guid? Id { get; set; }

        public string PaymentMethod { get; set; }

        public string AdditionalInformation { get; set; }

        public PersonSaveModel? Person { get; set; }

        public CompanySaveModel? Company { get; set; }
    }

    public class PersonSaveModel
    {
        public Guid? Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PersonalCode { get; set; }
    }

    public class CompanySaveModel
    {
        public Guid? Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }
    }
}
