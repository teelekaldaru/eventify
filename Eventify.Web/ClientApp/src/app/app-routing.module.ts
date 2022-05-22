import { EventCreateEditComponent } from './components/events/event-create-edit/event-create-edit.component';
import { EventsComponent } from './components/events/events.component';
import { Injectable, NgModule } from "@angular/core";
import { DefaultUrlSerializer, RouterModule, UrlSerializer, UrlTree } from "@angular/router";
import { Utilities } from "./services/utilities";
import { EventDetailsComponent } from './components/events/event-details/event-details.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { AttendeeDetailsComponent } from './components/attendees/attendee-details/attendee-details.component';

@Injectable()
export class LowerCaseUrlSerializer extends DefaultUrlSerializer {
    parse(url: string): UrlTree {
        const possibleSeparators = /[?;#]/;
        const indexOfSeparator = url.search(possibleSeparators);
        let processedUrl: string;

        if (indexOfSeparator > -1) {
            const separator = url.charAt(indexOfSeparator);
            const urlParts = Utilities.splitInTwo(url, separator);
            urlParts.firstPart = urlParts.firstPart.toLowerCase();

            processedUrl = urlParts.firstPart + separator + urlParts.secondPart;
        } else {
            processedUrl = url.toLowerCase();
        }

        return super.parse(processedUrl);
    }
}

const routes = [
    { path: '', component: EventsComponent, pathMatch: 'full' },
    {
        path: 'event',
        pageUniqueId: 'event',
        children: [
            { path: '', component: EventsComponent, pathMatch: 'full' },
            {
                path: 'save',
                component: EventCreateEditComponent,
                data: { title: 'Save event' },
                pageUniqueId: 'event-create-edit',
            },
            {
                path: ':id',
                component: EventDetailsComponent,
                data: { title: 'Event' },
                pageUniqueId: 'event-details',
            },
            {
                path: 'attendee/:id',
                component: AttendeeDetailsComponent,
                data: { title: 'Attendee' },
                pageUniqueId: 'attendee-details'
            },
        ],
    },
    { path: 'home', redirectTo: '/', pathMatch: 'full' },
    {
        path: '**',
        component: NotFoundComponent,
        data: { title: 'Page Not Found' },
    },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
    providers: [
        { provide: UrlSerializer, useClass: LowerCaseUrlSerializer },
    ],
})
export class AppRoutingModule {}
