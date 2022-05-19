import { AttendeeService } from './../../services/attendees/attendee.service';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AttendeeGridRow } from '../../models/attendees/attendee-grid-view.model';
import { Router } from '@angular/router';
import { first, map } from 'rxjs/operators';

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
        private readonly attendeeService: AttendeeService
    ) {}

    ngOnInit(): void {}

    openDetails(id: string): void {
        this.router.navigateByUrl(`event/attendee/${id}`);
    }

    delete(id: string): void {
        this.attendeeService
        .deleteAttendee(id)
            .pipe(
                first(),
                map((response) => {
                    if (response && response.success) {
                        this.onDeleted.emit(id);
                    } else {
                        console.log(response.messages);
                    }
                })
            )
            .subscribe();
    }
}
