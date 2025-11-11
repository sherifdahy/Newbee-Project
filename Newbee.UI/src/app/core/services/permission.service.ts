import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { Permissions } from '../enums/permissions.enum';
import { Role } from '../enums/role.enum';
import { ApiCallService } from './api-call.service';
import { catchError, map, Observable, retry, throwError } from 'rxjs';


@Injectable({ providedIn: 'root' })
export class PermissionService {
  constructor(private apiCall: ApiCallService, private auth: AuthService) { }

  getAll(): Observable<string[]> {
    return this.apiCall.get('api/permissions').pipe(
      map((response) => {
        return response as string[];
      }),
      catchError((response) => {
        return throwError(() => response.error.errors);
      }
      )
    )
}

// هل عنده الصلاحية دي؟
can(permission: Permissions): boolean {
  const user = this.auth.currentUser;
  if (!user) return false;
  return user.permissions.includes(permission);
}

// هل عنده أي صلاحية من دول؟
canAny(permissions: Permissions[]): boolean {
  const user = this.auth.currentUser;
  if (!user) return false;
  return permissions.some(p => user.permissions.includes(p));
}


}
