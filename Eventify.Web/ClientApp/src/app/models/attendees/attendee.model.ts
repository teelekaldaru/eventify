export class Attendee {
    id: string;
    firstName: string;
    lastName: string;
    name: string;
    code: string;
    attendeeType: AttendeeType;
}

export enum AttendeeType {
    Unknown = 0,
    Person = 10,
    Company = 20
}
