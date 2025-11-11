import { Injectable } from '@angular/core';
import { ApiCallService } from './api-call.service';
import { catchError, map, Observable, retry, throwError } from 'rxjs';
import { CityResponse } from '../models/city/responses/city-response';
import { CityRequest } from '../models/city/requests/city-request';

@Injectable({
  providedIn: 'root'
})
export class CityService {

constructor(private apiCall : ApiCallService){

  }

  getAll(): Observable<CityResponse[]> {
    return this.apiCall.get(`api/cities`).pipe(
      map((response) => {
        return response as CityResponse[];
      }),
      catchError((response) => {
        return throwError(() => response.error.errors);
      })
    )
  }

  getRelated(governorateId : number): Observable<CityResponse[]> {
    return this.apiCall.get(`api/cities/get-related?governorateId=${governorateId}`).pipe(
      map((response) => {
        return response as CityResponse[];
      }),
      catchError((response) => {
        return throwError(() => response.error.errors);
      })
    )
  }

  get(id: number): Observable<CityResponse> {
    return this.apiCall.get(`api/cities/${id}`).pipe(
      map((response) => {
        return response as CityResponse;
      }),
      catchError((response) => {
        return throwError(() => response.error.errors);
      })

    )
  }

  update(id : number,request : CityRequest)
  {
    return this.apiCall.put(`api/cities/${id}`,request).pipe(
      retry(3),
      catchError((response)=>{
        return throwError(()=> response.error.erros);
      })
    )
  }

  create(request: CityRequest) {
    return this.apiCall.post('api/cities', request).pipe(
      retry(3),
      catchError((response) => {
        return throwError(() => response.error.errors);
      })
    )
  }
}
