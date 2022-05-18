import { Injectable } from '@angular/core';
import { CommonEndpointService } from '../common-endpoint.service';

@Injectable()
export class AttendeeService {
    constructor(private readonly endpoint: CommonEndpointService) {}

    private readonly relativeUrl = '/api/attendees';
}
