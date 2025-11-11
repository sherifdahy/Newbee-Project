import { Injectable } from '@angular/core';
import { catchError, map, Observable, retry, throwError } from 'rxjs';
import { ApiCallService } from './api-call.service';
import { CountryResponse } from '../models/country/responses/country-response';
import { CountryRequest } from '../models/country/requests/country-request';

@Injectable({
  providedIn: 'root'
})
export class CountryService {

  constructor(private apiCall : ApiCallService){

  }

  getAll(): Observable<CountryResponse[]> {
    return this.apiCall.get(`api/countries`).pipe(
      map((response) => {
        return response as CountryResponse[];
      }),
      catchError((response) => {
        return throwError(() => response.error.errors);
      })
    )
  }

  get(id: number): Observable<CountryResponse> {
    return this.apiCall.get(`api/countries/${id}`).pipe(
      map((response) => {
        return response as CountryResponse;
      }),
      catchError((response) => {
        return throwError(() => response.error.errors);
      })

    )
  }

  update(id : number,countryRequest : CountryRequest)
  {
    return this.apiCall.put(`api/countries/${id}`,countryRequest).pipe(
      retry(3),
      catchError((response)=>{
        return throwError(()=> response.error.erros);
      })
    )
  }

  create(countryRequest: CountryRequest) {
    return this.apiCall.post('api/countries', countryRequest).pipe(
      retry(3),
      catchError((response) => {
        return throwError(() => response.error.errors);
      })
    )
  }


}
