import { AttendeeGridRow } from "../attendee/attendee-grid-view.model";

export class Event {
    id: string;
    name: string;
    startDate: Date;
    address: string;
    description?: string;
    attendees: AttendeeGridRow[];
}
