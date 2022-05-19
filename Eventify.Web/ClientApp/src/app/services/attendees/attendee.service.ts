import { Injectable } from '@angular/core';
import { AttendeeSave } from '../../models/attendee/attendee-save.model';
import { Attendee } from '../../models/attendee/attendee.model';
import { CommonEndpointService } from '../common-endpoint.service';

@Injectable()
export class AttendeeService {
    constructor(private readonly endpoint: CommonEndpointService) {}

    private readonly relativeUrl = '/api/attendees';

    getAttendee(id: string) {
        return this.endpoint.get<Attendee>(`${this.relativeUrl}/${id}`);
    }

    saveAttendee(data: AttendeeSave) {
        return this.endpoint.post<Attendee>(`${this.relativeUrl}`, data);
    }

    deleteAttendee(id: string) {
        return this.endpoint.delete<void>(`${this.relativeUrl}/${id}`);
    }
}
