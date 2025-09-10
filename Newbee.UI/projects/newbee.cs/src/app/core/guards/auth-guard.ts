import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth/auth.service';
import { AuthStatus } from '../enums/authstatus.enum';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);
  if (
    authService.authStatus === AuthStatus.refreshTokenExpired ||
    authService.authStatus === AuthStatus.emptyTokens
  ) {
    router.navigate(['/auth/login']);
    return false;
  }
  return true;
};
