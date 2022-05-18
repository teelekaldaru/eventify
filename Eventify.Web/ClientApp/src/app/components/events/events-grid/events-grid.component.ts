import { Component, Input, OnInit } from '@angular/core';
import { EventGridRow } from 'src/app/models/events/event-grid-view.model';

@Component({
  selector: 'events-grid',
  templateUrl: './events-grid.component.html',
  styleUrls: ['./events-grid.component.scss']
})
export class EventsGridComponent implements OnInit {

  @Input() title: string;
  @Input() events: EventGridRow[];
  @Input() canDelete: boolean;

  constructor() { }

  ngOnInit(): void {
  }

}
