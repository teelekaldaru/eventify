import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { first, map } from 'rxjs/operators';
import { Attendee } from 'src/app/models/attendees/attendee.model';
import { AttendeeService } from 'src/app/services/attendees/attendee.service';

@Component({
  selector: 'attendee-details',
  templateUrl: './attendee-details.component.html',
  styleUrls: ['./attendee-details.component.scss']
})
export class AttendeeDetailsComponent implements OnInit {

  id?: string;
  attendee: Attendee;

  constructor(
    private readonly route: ActivatedRoute,
    private readonly router: Router,
    private readonly attendeeService: AttendeeService
  ) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.params["id"];
    if (!!this.id) {
      this.getAttendee();
    }
  }

  back(): void {
    this.router.navigateByUrl(`event/${this.attendee.eventId}`);
  }

  private getAttendee(): void {
    this.attendeeService
      .getAttendee(this.id)
      .pipe(
        first(),
        map((response) => {
          if (response && response.success) {
            this.attendee = response.data;
          }
        })
      )
      .subscribe();
  }
}
