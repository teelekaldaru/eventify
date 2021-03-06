import { Router } from '@angular/router';
import { Component, Input, OnInit } from '@angular/core';
import { first, map } from 'rxjs/operators';
import { EventGridRow, EventGridView } from 'src/app/models/events/event-grid-view.model';
import { EventService } from 'src/app/services/events/event.service';
import { AlertService } from 'src/app/services/alerts/alert.service';

@Component({
    selector: 'events',
    templateUrl: './events.component.html',
    styleUrls: ['./events.component.scss'],
})
export class EventsComponent implements OnInit {
    events: EventGridView;

    constructor(
        private readonly eventService: EventService,
        private readonly alertService: AlertService,
        private readonly router: Router
    ) {}

    ngOnInit(): void {
        this.getEvents();
    }

    addEvent(): void {
        this.router.navigateByUrl("event/save");
    }

    removeEvent(id: string): void {
        const index = this.events.futureEvents.findIndex(x => x.id === id);
        if (index > -1) {
            this.events.futureEvents.splice(index, 1);
        }
    }

    private getEvents(): void {
        this.eventService.getEvents().pipe(
            first(),
            map((response) => {
                if (response && response.success) {
                    this.events = response.data;
                } else {
                    this.alertService.responseErrors(response.messages);
                }
            })
        ).subscribe();
    }

    get futureEvents(): EventGridRow[] { return this.events.futureEvents; }
    get pastEvents(): EventGridRow[] { return this.events.pastEvents; }
}
