import { Injectable } from '@angular/core';
import { ApiCallService } from './api-call.service';
import { catchError, map, Observable, throwError } from 'rxjs';
import { CompanyResponse } from '../models/company/responses/company-response';
import { CompanyRequest } from '../models/company/requests/company-request';

@Injectable({
  providedIn: 'root'
})
export class CompanyService {
  constructor(
    private apiCall: ApiCallService
  ) { }

  getAll(): Observable<CompanyResponse[]> {
    return this.apiCall.get('api/companies').pipe(
      map((response) => {
        return response as CompanyResponse[];
      }),
      catchError((response) => {
        return throwError(() => response.error.errors);
      })
    )
  }

  get(id : number): Observable<CompanyResponse>{
    return this.apiCall.get(`api/companies/${id}`).pipe(
      map((response)=>{
        return response as CompanyResponse;
      }),
      catchError((response)=>{
        return throwError(()=> response.error.errors)
      })
    );
  }

  create(request: CompanyRequest) {
    return this.apiCall.post('api/companies', request).pipe(
      catchError((response) => {
        return throwError(() => response.error.errors);
      })
    )
  }


  update(id : number,request : CompanyRequest)
  {
    return this.apiCall.put(`api/companies/${id}`,request).pipe(
      catchError((response)=>{
        return throwError(()=> response.error.errors);
      })
    )
  }
}
