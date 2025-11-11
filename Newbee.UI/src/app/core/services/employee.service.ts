import { Injectable } from '@angular/core';
import { ApiCallService } from './api-call.service';
import { catchError, Observable, throwError } from 'rxjs';
import { EmployeeResponse } from '../models/employee/responses/employee-response';
import { EmployeeRequest } from '../models/employee/requests/employee-request';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor(private apiCallService: ApiCallService) { }

  getAll(branchId: number): Observable<EmployeeResponse[]> {
    return this.apiCallService.get<EmployeeResponse[]>(`api/branches/${branchId}/employees`).pipe(
      catchError((response) => {
        return throwError(() => response.error.errors);
      })
    );
  }

  create(branchId: number, request: EmployeeRequest): Observable<EmployeeResponse> {
    return this.apiCallService.post<EmployeeResponse>(`api/branches/${branchId}/employees`, request).pipe(
      catchError((response) => {
        return throwError(() => response.error.errors);
      })
    );
  }

  update(branchId: number, request: EmployeeRequest): Observable<any> {
    return this.apiCallService.put(`api/branches/${branchId}/employees`, request).pipe(
      catchError((response) => {
        return throwError(() => response.error.errors);
      })
    );
  }

  delete(employeeId: number): Observable<any> {
    return this.apiCallService.delete(`api/employees/${employeeId}`).pipe(
      catchError((response) => {
        return throwError(() => response.error.errors);
      })
    );
  }


  get(employeeId: number): Observable<EmployeeResponse> {
    return this.apiCallService.get<EmployeeResponse>(`api/employees/${employeeId}`).pipe(
      catchError((response) => {
        return throwError(() => response.error.errors);
      })
    );
  }

}
