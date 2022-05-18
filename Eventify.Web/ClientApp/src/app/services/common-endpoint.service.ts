import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { RequestResult } from '../models/system/request-result';
import { ConfigurationService } from './configuration.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError } from 'rxjs/operators';

@Injectable()
export class CommonEndpointService {
    get baseUrl() {
        return this.configurations.baseUrl;
    }

    constructor(
        private readonly configurations: ConfigurationService,
        private readonly httpClient: HttpClient
    ) {}

    get<T>(relativeEndpointUrl: string): Observable<RequestResult<T>> {
        const endpointUrl = `${this.baseUrl}${relativeEndpointUrl}`;
        const headers = this.requestHeaders;
        return this.httpClient
            .get<RequestResult<T>>(endpointUrl, headers)
            .pipe<RequestResult<T>>(
                catchError((error) => {
                    return throwError(error);
                })
            );
    }

    post<T>(
        relativeEndpointUrl: string,
        data: object
    ): Observable<RequestResult<T>> {
        const endpointUrl = `${this.baseUrl}${relativeEndpointUrl}`;
        const headers = this.requestHeaders;
        return this.httpClient
            .post<RequestResult<T>>(endpointUrl, JSON.stringify(data), headers)
            .pipe<RequestResult<T>>(
                catchError((error) => {
                    return throwError(error);
                })
            );
    }

    delete<T>(relativeEndpointUrl: string): Observable<RequestResult<T>> {
        const endpointUrl = `${this.baseUrl}${relativeEndpointUrl}`;
        const headers = this.requestHeaders;
        return this.httpClient
            .delete<RequestResult<T>>(endpointUrl, headers)
            .pipe<RequestResult<T>>(
                catchError((error) => {
                    return throwError(error);
                })
            );
    }

    private get requestHeaders(): {
        headers: HttpHeaders | { [header: string]: string | string[] };
    } {
        const headers = new HttpHeaders({
            'Content-Type': 'application/json',
            Accept: 'application/json, text/plain, */*',
        });

        return { headers };
    }
}
