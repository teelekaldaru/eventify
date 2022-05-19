import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { first, map } from 'rxjs/operators';
import { EventSave } from 'src/app/models/events/event-save.model';
import { EventService } from 'src/app/services/events/event.service';

@Component({
    selector: 'event-create-edit',
    templateUrl: './event-create-edit.component.html',
    styleUrls: ['./event-create-edit.component.scss'],
})
export class EventCreateEditComponent implements OnInit {
    event: EventSave;

    constructor(
        private readonly eventService: EventService,
        private readonly router: Router
    ) {}

    ngOnInit(): void {
        this.event = {};
    }

    save(): void {
        console.log(this.event);
        this.eventService
            .saveEvent(this.event)
            .pipe(
                first(),
                map((response) => {
                    if (response && response.success) {
                        this.back();
                    } else {
                        console.log(response.messages);
                    }
                })
            )
            .subscribe();
    }

    back(): void {
        this.router.navigateByUrl("/");
    }

    get minDate(): string { return new Date().toISOString().slice(0, 16); }
}
