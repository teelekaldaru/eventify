import { AttendeeGridViewModel } from "../attendee/attendee-grid-view.model";

export class EventViewModel {
    id: string;
    name: string;
    startDate: Date;
    address: string;
    description?: string;
    attendees: AttendeeGridViewModel[];
}