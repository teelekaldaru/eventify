import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { first, map } from 'rxjs/operators';
import { EventGridRow } from 'src/app/models/events/event-grid-view.model';
import { AlertService } from 'src/app/services/alerts/alert.service';
import { EventService } from 'src/app/services/events/event.service';
import { ModalService } from 'src/app/services/modal.service';
import { ConfirmationDialogComponent } from '../../confirmation-dialog/confirmation-dialog.component';

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
        private readonly modalService: ModalService,
        private readonly router: Router
    ) {}

    ngOnInit(): void {}

    openDetails(id: string): void {
        this.router.navigateByUrl(`event/${id}`);
    }

    tryDelete(id: string): void {
        const initialState = {
            content: "Kas oled kindel, et soovid selle ürituse kustutada?",
            okLabel: "Jah, kustuta",
            cancelLabel: "Ei, tühista",
            onConfirm: () => this.delete(id)
        };
        this.modalService.openModal(ConfirmationDialogComponent, initialState);
    }

    private delete(id: string): void {
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
