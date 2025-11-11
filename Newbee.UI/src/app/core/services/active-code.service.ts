import { Injectable } from '@angular/core';
import { catchError, map, Observable, retry, throwError } from 'rxjs';
import { ActiveCodeResponse } from '../models/active-code/response/active-code-response';
import { HttpErrorResponse } from '@angular/common/http';
import { ActiveCodeRequest } from '../models/active-code/request/active-code-request';
import { ApiCallService } from './api-call.service';

@Injectable({
  providedIn: 'root'
})
export class ActiveCodeService {

  constructor(private apiCall: ApiCallService) {

  }

  create(body: ActiveCodeRequest): Observable<void> {
    return this.apiCall.post<void>('api/activecodes', body).pipe(
      catchError((response: HttpErrorResponse) => {
        return throwError(() => response.error.errors)
      })
    )
  }

  update(id: number, body: ActiveCodeRequest) {
    return this.apiCall.put(`api/activecodes/${id}`, body).pipe(
      catchError((response: HttpErrorResponse) => {
        return throwError(() => response.error.errors)
      })
    )
  }
  get(id: number): Observable<ActiveCodeResponse> {
    return this.apiCall.get<ActiveCodeResponse>(`api/activecodes/${id}`).pipe(
      map((response) => {
        return response as ActiveCodeResponse;
      }),
      catchError((response) => {
        return throwError(() => response.error.errors);
      })
    )
  }
  getAll(): Observable<ActiveCodeResponse[]> {
    return this.apiCall.get<ActiveCodeResponse[]>('api/activecodes').pipe(
      map((response) => {
        return response as ActiveCodeResponse[];
      }),
      catchError((response) => {
        return throwError(() => response.error.errors);
      })
    )
  }

  delete(id: number): Observable<void> {
    return this.apiCall.delete(`activecodes/${id}`).pipe(
      catchError((response: HttpErrorResponse) => {
        return throwError(() => response.error.errors)
      })
    );
  }
}
