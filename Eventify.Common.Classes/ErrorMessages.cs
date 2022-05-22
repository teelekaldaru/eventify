namespace Eventify.Common.Classes
{
    public static class ErrorMessages
    {
        public const string EventNameRequired = "Nimi on kohustuslik";
        public const string EventAddressRequired = "Koht on kohustuslik";
        public const string EventStartTimeRequired = "Toimumisaeg on kohustuslik";
        public const string EventStartTimeInvalidFormat = "Toimumisaeg ei ole sisestatud korrektses formaadis";

        public const string AttendeeTypeInvalid = "Osavõtja tüüp ei ole täpsustatud";
        public const string AttendeeFirstNameRequired = "Eesnimi on kohustuslik";
        public const string AttendeeLastNameRequired = "Perenimi on kohustuslik";
        public const string AttendeeNameRequired = "Nimi on kohustuslik";
        public const string AttendeePersonalCodeRequired = "Isikukood on kohustuslik";
        public const string AttendeePersonalCodeInvalid = "Isikukood ei ole korrektne";
        public const string AttendeeRegisterCodeRequired = "Registrikood on kohustuslik";
        public const string AttendeeParticipantsRequired = "Osalejate arv on kohustuslik";
        public const string AttendeeParticipantsInvalid = "Osalejate arv peab olema vähemalt 1";
        public const string AttendeePaymentMethodRequired = "Maksmisviis on kohustuslik";
    }
}
