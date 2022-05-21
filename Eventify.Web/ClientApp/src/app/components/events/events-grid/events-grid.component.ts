import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { first, map } from 'rxjs/operators';
import { EventGridRow } from 'src/app/models/events/event-grid-view.model';
import { AlertService } from 'src/app/services/alert.service';
import { EventService } from 'src/app/services/events/event.service';

@Component({
    selector: 'events-grid',
    templateUrl: './events-grid.component.html',
    styleUrls: ['./events-grid.component.scss'],
})
export class EventsGridComponent implements OnInit {
    @Input() title: string;
    @Input() events: EventGridRow[];
    @Input() canDelete: boolean;

    @Output() onDeleted: EventEmitter<string> = new EventEmitter();

    constructor(
        private readonly eventService: EventService,
        private readonly alertService: AlertService,
        private readonly router: Router
    ) {}

    ngOnInit(): void {}

    openDetails(id: string): void {
        this.router.navigateByUrl(`event/${id}`);
    }

    deleteEvent(id: string): void {
        this.eventService
            .deleteEvent(id)
            .pipe(
                first(),
                map((response) => {
                    if (response && response.success) {
                        this.onDeleted.emit(id);
                    } else {
                        this.alertService.responseErrors(response.messages);
                    }
                })
            )
            .subscribe();
    }
}
