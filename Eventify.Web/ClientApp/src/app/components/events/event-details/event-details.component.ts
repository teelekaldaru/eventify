import { Event } from 'src/app/models/events/event.model';
import { Component, OnInit } from '@angular/core';
import { EventService } from '../../../services/events/event.service';
import { ActivatedRoute, Router } from '@angular/router';
import { first, map } from 'rxjs/operators';
import { AttendeeGridRow } from '../../../models/attendees/attendee-grid-view.model';
import { AlertService } from 'src/app/services/alert.service';

@Component({
    selector: 'event-details',
    templateUrl: './event-details.component.html',
    styleUrls: ['./event-details.component.scss'],
})
export class EventDetailsComponent implements OnInit {

    id?: string;
    event: Event;

    constructor(
        private readonly route: ActivatedRoute,
        private readonly router: Router,
        private readonly eventService: EventService,
        private readonly alertService: AlertService
    ) {}

    ngOnInit(): void {
        this.id = this.route.snapshot.params["id"];
        if (!!this.id) {
            this.getEventDetails();
        }
    }

    addAttendee(attendee: AttendeeGridRow) {
        this.event.attendees.push(attendee);
    }

    removeAttendee(attendeeId: string) {
        const index = this.event.attendees.findIndex(x => x.id === attendeeId);
        if (index > -1) {
            this.event.attendees.splice(index, 1);
        }
    }

    private getEventDetails(): void {
        this.eventService
            .getEvent(this.id)
                .pipe(
                    first(),
                    map((response) => {
                        if (response && response.success) {
                            this.event = response.data;
                        } else {
                            this.alertService.responseErrors(response.messages);
                        }
                    })
                )
                .subscribe();
    }

    get hasAttendees(): boolean { return !!this.event && this.event.attendees.length > 0; }
}
