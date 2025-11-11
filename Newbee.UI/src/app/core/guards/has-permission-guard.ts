import { CanActivateFn, Router } from '@angular/router';
import { PermissionService } from '../services/permission.service';
import { inject } from '@angular/core';

export const hasPermissionGuard: CanActivateFn = (route, state) => {
  const permissionService = inject(PermissionService);
  const router = inject(Router);

  let allowedPermissions = route.data['permissions'];

  if(permissionService.canAny(allowedPermissions))
    return true;

  router.navigate(['/no-access']);
  return false;
};
