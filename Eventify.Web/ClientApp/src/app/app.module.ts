import { EventDetailsComponent } from './components/events/event-details/event-details.component';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './components/app.component';
import { EventsComponent } from './components/events/events.component';
import { AttendeeService } from './services/attendees/attendee.service';
import { CommonEndpointService } from './services/common-endpoint.service';
import { ConfigurationService } from './services/configuration.service';
import { EventService } from './services/events/event.service';
import { EventsGridComponent } from './components/events/events-grid/events-grid.component';
import { EventCreateEditComponent } from './components/events/event-create-edit/event-create-edit.component';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { CommonModule } from '@angular/common';
import { AttendeesGridComponent } from './components/attendees/attendees-grid/attendees-grid.component';
import { AttendeeCreateEditComponent } from './components/attendees/attendee-create-edit/attendee-create-edit.component';
import { FinanceService } from './services/finances/finance.service';
import { SubNavbarComponent } from './components/sub-navbar/sub-navbar.component';
import { AttendeeDetailsComponent } from './components/attendees/attendee-details/attendee-details.component';
import { AlertModule } from './services/alerts/alert.module';
import { AlertService } from './services/alerts/alert.service';
import { BackButtonDirective } from './directives/back-button.directive';
import { ModalService } from './services/modal.service';
import { ModalModule } from 'ngx-bootstrap/modal';

@NgModule({
    declarations: [
        AppComponent,
        EventsComponent,
        EventsGridComponent,
        EventCreateEditComponent,
        EventDetailsComponent,
        AttendeesGridComponent,
        AttendeeCreateEditComponent,
        SubNavbarComponent,
        AttendeeDetailsComponent,
        BackButtonDirective
    ],
    imports: [
        BrowserModule,
        HttpClientModule,
        FormsModule,
        AppRoutingModule,
        CommonModule,
        AlertModule,
        ModalModule.forRoot()
    ],
    providers: [
        EventService,
        AttendeeService,
        CommonEndpointService,
        ConfigurationService,
        FinanceService,
        AlertService,
        ModalService
    ],
    bootstrap: [AppComponent],
})
export class AppModule {}
