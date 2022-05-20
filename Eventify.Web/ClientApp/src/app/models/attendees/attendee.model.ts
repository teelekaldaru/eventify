export class Attendee {
    id: string;
    attendeeType: AttendeeType;
}

export class PersonAttendee extends Attendee {
    firstName: string;
    lastName: string;
    personalCode: string;
}

export class CompanyAttendee extends Attendee {
    name: string;
    registerCode: string;
}

export enum AttendeeType {
    Unknown = 0,
    Person = 10,
    Company = 20
}
