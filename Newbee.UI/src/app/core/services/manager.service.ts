import { Injectable } from '@angular/core';
import { ApiCallService } from './api-call.service';
import { ManagerRequest } from '../models/manager/requests/manager-request';
import { catchError, map, Observable, throwError } from 'rxjs';
import { environment } from '../../../environments/environment';
import { ManagerResponse } from '../models/manager/responses/manager-response';

@Injectable({
  providedIn: 'root'
})
export class ManagerService {

  constructor(private apiCall: ApiCallService) { }

  create(companyId: number, request: ManagerRequest) {
    return this.apiCall.post(`api/companies/${companyId}/managers`, request).pipe(
      catchError((response) => {
        return throwError(() => response.error.errors);
      })
    )
  }

  update(companyId : number,managerId:number,request :ManagerRequest)
  {
    return this.apiCall.put(`api/companies/${companyId}/managers/${managerId}`, request).pipe(
      catchError((response) => {
        return throwError(() => response.error.errors);
      })
    )
  }

  getAll(companyId: number): Observable<ManagerResponse[]> {
    return this.apiCall.get(`api/companies/${companyId}/managers`).pipe(
      map((response) => {
        return response as ManagerResponse[]
      }),
      catchError((response) => {
        return throwError(() => response.error.errors);
      })
    )
  }

  get(managerId: number): Observable<ManagerResponse> {
    return this.apiCall.get(`api/managers/${managerId}`).pipe(
      map((response) => {
        return response as ManagerResponse
      }),
      catchError((response) => {
        return throwError(() => response.error.errors);
      })
    )
  }

  delete(managerId: number) {
    return this.apiCall.delete(`api/managers/${managerId}`).pipe(
      catchError((response) => {
        return throwError(() => response.error.errors);
      })
    )
  }
}
