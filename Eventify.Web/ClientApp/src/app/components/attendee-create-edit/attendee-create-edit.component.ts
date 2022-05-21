import { FinanceService } from './../../services/finances/finance.service';
import { AttendeeService } from './../../services/attendees/attendee.service';
import { AttendeeSave } from '../../models/attendees/attendee-save.model';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { first, map } from 'rxjs/operators';
import { PaymentMethod } from '../../models/finances/payment-method.model';
import { AttendeeType } from '../../models/attendees/attendee.model';
import { AttendeeGridRow } from 'src/app/models/attendees/attendee-grid-view.model';
import { AlertService } from 'src/app/services/alert.service';

@Component({
    selector: 'attendee-create-edit',
    templateUrl: './attendee-create-edit.component.html',
    styleUrls: ['./attendee-create-edit.component.scss'],
})
export class AttendeeCreateEditComponent implements OnInit {

    @Input() eventId: string;
    @Input() attendee?: AttendeeSave;
    @Input() showButtons: boolean;

    @Output() onSave: EventEmitter<AttendeeGridRow> = new EventEmitter();

    attendeeType: AttendeeType = AttendeeType.Person;
    paymentMethods: PaymentMethod[] = [];

    constructor(
        private readonly attendeeService: AttendeeService,
        private readonly financeService: FinanceService,
        private readonly alertService: AlertService
    ) {}

    ngOnInit(): void {
        this.getPaymentMethods();
        if (!this.attendee) {
            this.resetForm();
        }
    }

    changeAttendeeType(type: AttendeeType): void {
        this.attendeeType = type;
        this.resetForm();
    }

    save(): void {
        this.alertService.clear();
        this.attendeeService
            .saveAttendee(this.attendee)
            .pipe(
                first(),
                map((response) => {
                    if (response && response.success) {
                        this.onSave.emit(response.data);
                    } else {
                        this.alertService.responseErrors(response.messages);
                    }
                })
            )
            .subscribe();
    }

    private resetForm(): void {
        this.attendee = {
            attendeeType: this.attendeeType,
            eventId: this.eventId
        };
    }

    private getPaymentMethods(): void {
        this.financeService
            .getPaymentMethods()
            .pipe(
                first(),
                map((response) => {
                    if (response && response.success) {
                        this.paymentMethods = response.data;
                    }
                })
            )
            .subscribe();
    }
}
