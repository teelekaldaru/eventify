import { FinanceService } from './../../services/finances/finance.service';
import { AttendeeService } from './../../services/attendees/attendee.service';
import { Router } from '@angular/router';
import { AttendeeSave } from '../../models/attendees/attendee-save.model';
import { Component, Input, OnInit } from '@angular/core';
import { first, map } from 'rxjs/operators';
import { PaymentMethod } from '../../models/finances/payment-method.model';
import { AttendeeType } from '../../models/attendees/attendee.model';

@Component({
    selector: 'attendee-create-edit',
    templateUrl: './attendee-create-edit.component.html',
    styleUrls: ['./attendee-create-edit.component.scss'],
})
export class AttendeeCreateEditComponent implements OnInit {

    @Input() attendeeId?: string;
    @Input() showButtons: boolean;

    attendee: AttendeeSave;
    attendeeType: AttendeeType = AttendeeType.Person;
    paymentMethods: PaymentMethod[] = [];

    constructor(
        private readonly router: Router,
        private readonly attendeeService: AttendeeService,
        private readonly financeService: FinanceService
    ) {}

    ngOnInit(): void {
        this.getPaymentMethods();
        if (!!this.attendeeId) {
            this.getAttendee();
        } else {
            this.resetForm();
        }
    }

    changeAttendeeType(type: AttendeeType): void {
        this.attendeeType = type;
        this.resetForm();
    }

    save(): void {

    }

    back(): void {
        this.router.navigateByUrl(`events`);
    }

    private resetForm(): void {
        this.attendee = {
            attendeeType: this.attendeeType
        };
    }

    private getAttendee(): void {
        this.attendeeService
            .getAttendee(this.attendeeId)
            .pipe(
                first(),
                map((response) => {
                    if (response && response.success) {
                        this.attendee = response.data;
                    } else {
                        console.log(response.messages);
                    }
                })
            )
            .subscribe();
    }

    private getPaymentMethods(): void {
        this.financeService
            .getPaymentMethods()
            .pipe(
                first(),
                map((response) => {
                    if (response && response.success) {
                        this.paymentMethods = response.data;
                    } else {
                        console.log(response.messages);
                    }
                })
            )
            .subscribe();
    }
}
