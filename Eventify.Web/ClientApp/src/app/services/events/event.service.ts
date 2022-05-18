import { Injectable } from '@angular/core';
import { CommonEndpointService } from '../common-endpoint.service';

@Injectable()
export class EventService {
    constructor(private readonly endpoint: CommonEndpointService) {}

    private readonly relativeUrl = '/api/events';
}
