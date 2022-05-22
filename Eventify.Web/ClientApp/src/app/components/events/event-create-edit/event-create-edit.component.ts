import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { first, map } from 'rxjs/operators';
import { EventSave } from 'src/app/models/events/event-save.model';
import { AlertService } from 'src/app/services/alerts/alert.service';
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
        private readonly router: Router,
        private readonly alertService: AlertService
    ) {}

    ngOnInit(): void {
        this.clearForm();
    }

    save(): void {
        this.alertService.clear();
        this.eventService
            .saveEvent(this.event)
            .pipe(
                first(),
                map((response) => {
                    if (response && response.success) {
                        this.clearForm();
                        this.router.navigateByUrl(`/`);
                    } else {
                        this.alertService.responseErrors(response.messages);
                    }
                })
            )
            .subscribe();
    }

    private clearForm(): void {
        this.event = {};
    }

    get minDate(): string { return new Date().toISOString().slice(0, 16); }
}
