import { Injectable } from '@angular/core';
import { Role } from '../enums/role.enum';
import { AuthService } from './auth.service';
import { ApiCallService } from './api-call.service';
import { catchError, map, Observable, retry, throwError } from 'rxjs';
import { RoleResponse } from '../models/role/responses/role-response';
import { RoleRequest } from '../models/role/requests/role-request';
import { RoleDetailResponse } from '../models/role/responses/role-detail-response';

@Injectable({
  providedIn: 'root'
})
export class RoleService {

  constructor(private apiCall: ApiCallService, private authService: AuthService) { }

  getAll(showDeleted: boolean): Observable<RoleResponse[]> {
    return this.apiCall.get(`api/roles?includeDisabled=${showDeleted}`).pipe(
      map((response) => {
        return response as RoleResponse[];
      }),
      catchError((response) => {
        return throwError(() => response.error.errors);
      })
    )
  }

  get(id: number): Observable<RoleDetailResponse> {
    return this.apiCall.get(`api/roles/${id}`).pipe(
      map((response) => {
        return response as RoleDetailResponse;
      }),
      catchError((response) => {
        return throwError(() => response.error.errors);
      })

    )
  }

  update(id : number,roleRequest : RoleRequest)
  {
    return this.apiCall.put(`api/roles/${id}`,roleRequest).pipe(
      retry(3),
      catchError((response)=>{
        return throwError(()=> response.error.erros);
      })
    )
  }

  create(roleRequest: RoleRequest) {
    return this.apiCall.post('api/roles', roleRequest).pipe(
      retry(3),
      catchError((response) => {
        return throwError(() => response.error.errors);
      })
    )
  }

  toggleState(id: number) {
    return this.apiCall.post(`api/roles/${id}/toggle-status`, {}).pipe(
      retry(3),
      catchError((response) => {
        return throwError(() => response.error.errors);
      })
    )
  }

  hasRole(role: Role): boolean {
    const user = this.authService.currentUser;
    if (!user) return false;
    return user.roles.includes(role);
  }

  hasAnyRole(roles: Role[]): boolean {
    const user = this.authService.currentUser;
    if (!user) return false;
    return roles.some(p => user.roles.includes(p));
  }
}
