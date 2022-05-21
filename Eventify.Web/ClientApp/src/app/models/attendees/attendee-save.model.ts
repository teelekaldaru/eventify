import { AttendeeType } from "./attendee.model";

export class AttendeeSave {
    id?: string;
    name?: string;
    lastName?: string;
    code?: string;
    paymentMethod?: string;
    participants?: number;
    notes?: string;
    eventId: string;
    attendeeType: AttendeeType;
}
