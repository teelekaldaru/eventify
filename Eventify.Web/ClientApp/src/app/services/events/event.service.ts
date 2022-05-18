import { Injectable } from '@angular/core';
import { EventGridViewModel } from 'src/app/models/events/event-grid-view.model';
import { EventSaveModel } from 'src/app/models/events/event-save.model';
import { EventViewModel } from 'src/app/models/events/event-view.model';
import { CommonEndpointService } from '../common-endpoint.service';

@Injectable()
export class EventService {
    constructor(private readonly endpoint: CommonEndpointService) {}

    private readonly relativeUrl = '/api/events';

    getEvents() {
        return this.endpoint.get<EventGridViewModel>(`${this.relativeUrl}`);
    }

    getEvent(id: string) {
        return this.endpoint.get<EventViewModel>(`${this.relativeUrl}/${id}`);
    }

    saveEvent(data: EventSaveModel) {
        return this.endpoint.post<EventViewModel>(`${this.relativeUrl}`, { data });
    }

    deleteEvent(id: string) {
        return this.endpoint.delete<void>(`${this.relativeUrl}/${id}`);
    }
}
