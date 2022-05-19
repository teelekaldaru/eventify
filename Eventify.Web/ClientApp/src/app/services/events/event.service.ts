import { Injectable } from '@angular/core';
import { EventGridView } from 'src/app/models/events/event-grid-view.model';
import { EventSave } from 'src/app/models/events/event-save.model';
import { Event } from 'src/app/models/events/event.model';
import { CommonEndpointService } from '../common-endpoint.service';

@Injectable()
export class EventService {
    constructor(private readonly endpoint: CommonEndpointService) {}

    private readonly relativeUrl = '/api/events';

    getEvents() {
        return this.endpoint.get<EventGridView>(`${this.relativeUrl}`);
    }

    getEvent(id: string) {
        return this.endpoint.get<Event>(`${this.relativeUrl}/${id}`);
    }

    saveEvent(data: EventSave) {
        return this.endpoint.post<Event>(`${this.relativeUrl}`, data);
    }

    deleteEvent(id: string) {
        return this.endpoint.delete<void>(`${this.relativeUrl}/${id}`);
    }
}
