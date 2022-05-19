import { Router } from '@angular/router';
import { AttendeeSave, AttendeeType } from './../../models/attendee/attendee-save.model';
import { Component, Input, OnInit } from '@angular/core';

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

    constructor(
        private readonly router: Router
    ) {}

    ngOnInit(): void {
        if (!!this.attendeeId) {
            this.getAttendee();
        } else {
            this.attendee = {
                person: {}
            };
        }
    }

    changeType(type: AttendeeType): void {
        this.attendeeType = type;
        switch (type) {
            case AttendeeType.Company:
                this.attendee.person = null;
                this.attendee.company = {};
                break;
            case AttendeeType.Person:
            case AttendeeType.Unknown:
            default:
                this.attendee.person = {};
                this.attendee.company = null;
        }
    }

    save(): void {

    }

    back(): void {
        this.router.navigateByUrl(`events`);
    }

    private getAttendee(): void {

    }
}
