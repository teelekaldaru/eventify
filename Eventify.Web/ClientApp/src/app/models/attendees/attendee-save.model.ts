import { AttendeeType } from "./attendee.model";

export class AttendeeSave {
    id?: string;
    name?: string;
    firstName?: string;
    lastName?: string;
    personalCode?: string;
    registerCode?: string;
    paymentMethod?: string;
    notes?: string;
    attendeeType: AttendeeType;
}
