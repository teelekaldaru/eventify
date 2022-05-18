import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './components/app.component';
import { EventsComponent } from './components/events/events.component';
import { AlertService } from './services/alert.service';
import { AttendeeService } from './services/attendees/attendee.service';
import { CommonEndpointService } from './services/common-endpoint.service';
import { ConfigurationService } from './services/configuration.service';
import { EventService } from './services/events/event.service';
import { EventsGridComponent } from './components/events/events-grid/events-grid.component';
import { EventCreateEditComponent } from './components/events/event-create-edit/event-create-edit.component';
import { FormsModule } from '@angular/forms';

@NgModule({
    declarations: [
        AppComponent,
        EventsComponent,
        EventsGridComponent,
        EventCreateEditComponent
    ],
    imports: [
        BrowserModule,
        HttpClientModule,
        FormsModule
    ],
    providers: [
        EventService,
        AttendeeService,
        AlertService,
        CommonEndpointService,
        ConfigurationService
    ],
    bootstrap: [AppComponent],
})
export class AppModule {}
