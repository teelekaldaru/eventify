import { AttendeeGridRow } from "../attendees/attendee-grid-view.model";

export class Event {
    id: string;
    name: string;
    startDate: Date;
    address: string;
    notes?: string;
    isPast: boolean;
    attendees: AttendeeGridRow[];
}
