import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Utilities } from './utilities';

@Injectable()
export class ConfigurationService {
    constructor() {}

    public baseUrl = environment.baseUrl || Utilities.baseUrl();
}
