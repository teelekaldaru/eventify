import { Component, Input, OnInit } from '@angular/core';
import { first, map } from 'rxjs/operators';
import { EventSaveModel } from 'src/app/models/events/event-save.model';
import { AlertService } from 'src/app/services/alert.service';
import { EventService } from 'src/app/services/events/event.service';

@Component({
  selector: 'event-create-edit',
  templateUrl: './event-create-edit.component.html',
  styleUrls: ['./event-create-edit.component.scss']
})
export class EventCreateEditComponent implements OnInit {

  event: EventSaveModel;

  constructor(
    private readonly eventService: EventService,
    private readonly alertService: AlertService
  ) { }

  ngOnInit(): void {
    this.event = {};
  }

  save() {
    this.eventService.saveEvent(this.event).pipe(
      first(),
      map((response) => {
        if (response && response.success) {
          console.log("Success");
        } else {
          this.alertService.showResponseMessages(response.messages);
        }
      })
    ).subscribe();
  }
}
