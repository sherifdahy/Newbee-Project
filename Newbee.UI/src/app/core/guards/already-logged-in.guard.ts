import { inject, Inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { Role } from '../enums/role.enum';

export const alreadyLoggedInGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);


  if(authService.isLoggedIn && !authService.isTokenExpired()) {
    if(authService.currentUser?.roles.includes(Role[Role.Admin]) || authService.currentUser?.roles.includes(Role[Role.Manager]))
    {
      router.navigateByUrl('partner/dashboard');
    }
    else{
      router.navigateByUrl('portal');
    }
    return false;
  }

  return true;
};
