export class EventGridView {
    pastEvents: EventGridRow[];
    futureEvents: EventGridRow[];
}

export class EventGridRow {
    id: string;
    name: string;
    startDate: Date;
    isPast: boolean;
}
