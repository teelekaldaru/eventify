import { AlertService } from 'src/app/services/alert.service';
import { EventViewModel } from 'src/app/models/events/event-view.model';
import { Component, OnInit } from '@angular/core';
import { EventService } from '../../../services/events/event.service';
import { ActivatedRoute } from '@angular/router';
import { first, map } from 'rxjs/operators';

@Component({
    selector: 'event-details',
    templateUrl: './event-details.component.html',
    styleUrls: ['./event-details.component.scss'],
})
export class EventDetailsComponent implements OnInit {

    id?: string;
    event: EventViewModel;

    constructor(
        private readonly route: ActivatedRoute,
        private readonly eventService: EventService,
        private readonly alertService: AlertService
    ) {}

    ngOnInit(): void {
        this.id = this.route.snapshot.params["id"];
        console.log(this.id)
        if (!!this.id) {
            this.getEventDetails();
        }
    }

    private getEventDetails(): void {
        this.eventService
            .getEvent(this.id)
                .pipe(
                    first(),
                    map((response) => {
                        if (response && response.success) {
                            this.event = response.data;
                        } else {
                            this.alertService.showResponseMessages(
                                response.messages
                            );
                        }
                    })
                )
                .subscribe();
    }
}
