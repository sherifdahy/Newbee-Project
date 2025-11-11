import { Injectable } from '@angular/core';
import { ApiCallService } from './api-call.service';
import { GovernorateResponse } from '../models/governorate/responses/governorate-response';
import { catchError, map, Observable, retry, throwError } from 'rxjs';
import { GovernorateRequest } from '../models/governorate/requests/governorate-request';

@Injectable({
  providedIn: 'root'
})
export class GovernorateService {

  constructor(private apiCall: ApiCallService) {

  }

  getAll(): Observable<GovernorateResponse[]> {
    return this.apiCall.get(`api/governates`).pipe(
      map((response) => {
        return response as GovernorateResponse[];
      }),
      catchError((response) => {
        return throwError(() => response.error.errors);
      })
    )
  }

  getRelated(countryId : number): Observable<GovernorateResponse[]> {
    return this.apiCall.get(`api/governates/get-related?countryId=${countryId}`).pipe(
      map((response) => {
        return response as GovernorateResponse[];
      }),
      catchError((response) => {
        return throwError(() => response.error.errors);
      })
    )
  }



  get(id: number): Observable<GovernorateResponse> {
    return this.apiCall.get(`api/governates/${id}`).pipe(
      map((response) => {
        return response as GovernorateResponse;
      }),
      catchError((response) => {
        return throwError(() => response.error.errors);
      })

    )
  }

  update(id: number, request: GovernorateRequest) {
    return this.apiCall.put(`api/governates/${id}`, request).pipe(
      catchError((response) => {
        return throwError(() => response.error.erros);
      })
    )
  }

  create(request: GovernorateRequest) {
    return this.apiCall.post('api/governates', request).pipe(
      catchError((response) => {
        return throwError(() => response.error.errors);
      })
    )
  }

}
