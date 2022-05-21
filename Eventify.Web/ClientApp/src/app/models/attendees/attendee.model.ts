export class Attendee {
    id: string;
    eventId: string;
    code: string;
    name: string;
    lastName?: string;
    notes?: string;
    paymentMethod: string;
    attendeeType: AttendeeType;
    participants?: number;
}

export enum AttendeeType {
    Unknown = 0,
    Person = 10,
    Company = 20
}
