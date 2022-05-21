import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
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
  isEditMode: boolean = false;

  constructor(
    private readonly route: ActivatedRoute,
    private readonly attendeeService: AttendeeService
  ) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.params["id"];
    if (!!this.id) {
      this.getAttendee();
    }
  }

  edit(): void {
    this.isEditMode = true;
  }

  onSaved(): void {
    this.isEditMode = false;
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
