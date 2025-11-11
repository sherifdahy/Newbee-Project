import { Injectable } from '@angular/core';
import { ApiCallService } from './api-call.service';
import { PointOfSaleRequest } from '../models/point-of-sale/requests/point-of-sale-request';
import { catchError, Observable, throwError } from 'rxjs';
import { PointOfSaleResponse } from '../models/point-of-sale/responses/point-of-sale-response';

@Injectable({
  providedIn: 'root'
})
export class PointOfSaleService {

  constructor(
    private apiCallService: ApiCallService,

  ) { }

  create(branchId: number, request: PointOfSaleRequest): Observable<PointOfSaleResponse> {
    return this.apiCallService.post<PointOfSaleResponse>(`api/branches/${branchId}/point-of-sales`, request).pipe(
      catchError((response) => {
        return throwError(() => response.error.errors);
      })
    );
  }
  get(posId: number): Observable<PointOfSaleResponse> {
    return this.apiCallService.get<PointOfSaleResponse>(`api/point-of-sales/${posId}`).pipe(
      catchError((response) => {
        return throwError(() => response.error.errors);
      })
    );
  }

  getAll(branchId : number): Observable<PointOfSaleResponse[]> {
    return this.apiCallService.get<PointOfSaleResponse[]>(`api/branches/${branchId}/point-of-sales`).pipe(
      catchError((response) => {
        return throwError(() => response.error.errors);
      })
    );
  }


  update(posId: number, request: PointOfSaleRequest): Observable<any> {
    return this.apiCallService.put(`api/point-of-sales/${posId}`, request).pipe(
      catchError((response) => {
        return throwError(() => response.error.errors);
      }
      ));
  }

  delete(posId: number): Observable<void> {
    return this.apiCallService.delete(`api/point-of-sales/${posId}`).pipe(
      catchError((response) => {
        return throwError(() => response.error.errors);
      })
    );
  }
}
