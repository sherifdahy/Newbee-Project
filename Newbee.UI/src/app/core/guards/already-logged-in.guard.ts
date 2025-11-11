import { inject, Inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { Role } from '../enums/role.enum';

export const alreadyLoggedInGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);


  if(authService.isLoggedIn && !authService.isTokenExpired()) {
    if(authService.currentUser?.roles.includes(Role[Role.Admin]))
    {
      router.navigateByUrl('admin');
    }
    else{
      router.navigateByUrl('portal');
    }
    return false;
  }

  return true;
};
