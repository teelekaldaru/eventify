import { AttendeeService } from '../../../services/attendees/attendee.service';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AttendeeGridRow } from '../../../models/attendees/attendee-grid-view.model';
import { Router } from '@angular/router';
import { first, map } from 'rxjs/operators';
import { AlertService } from 'src/app/services/alerts/alert.service';
import { ConfirmationDialogComponent } from '../../confirmation-dialog/confirmation-dialog.component';
import { ModalService } from 'src/app/services/modal.service';

@Component({
    selector: 'attendees-grid',
    templateUrl: './attendees-grid.component.html',
    styleUrls: ['./attendees-grid.component.scss'],
})
export class AttendeesGridComponent implements OnInit {

    @Input() attendees: AttendeeGridRow[];
    @Output() onDeleted: EventEmitter<string> = new EventEmitter();

    constructor(
        private readonly router: Router,
        private readonly attendeeService: AttendeeService,
        private readonly alertService: AlertService,
        private readonly modalService: ModalService
    ) {}

    ngOnInit(): void {}

    openDetails(id: string): void {
        this.router.navigateByUrl(`event/attendee/${id}`);
    }

    tryDelete(id: string): void {
        const initialState = {
            content: "Kas oled kindel, et soovid osavõtja ürituselt eemaldada?",
            okLabel: "Jah, eemalda",
            cancelLabel: "Ei, tühista",
            onConfirm: () => this.delete(id)
        };
        this.modalService.openModal(ConfirmationDialogComponent, initialState);
    }

    private delete(id: string): void {
        this.attendeeService
            .deleteAttendee(id)
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
