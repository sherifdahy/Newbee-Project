import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { Inject, inject } from '@angular/core';
import { RoleService } from '../services/role.service';

export const hasRoleGuard: CanActivateFn = (route, state) => {
  const roleService = inject(RoleService);
  const router = inject(Router);

  const allowedRoles = route.data['roles'];

  if (roleService.hasAnyRole(allowedRoles))
    return true;

  router.navigate(['/no-access']);
  return false;
};
