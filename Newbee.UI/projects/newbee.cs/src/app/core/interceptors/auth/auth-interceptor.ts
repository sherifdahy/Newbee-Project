import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpInterceptorFn,
  HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from '../../services/backend/auth/auth.service';
import { Router } from '@angular/router';
import { Observable, throwError, catchError, switchMap } from 'rxjs';
import { AuthStatus } from '../../enums/authstatus.enum';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService, private router: Router) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const status = this.authService.authStatus;

    if (status === AuthStatus.emptyTokens) {
      return next.handle(req);
    }

    if (req.url.includes('/Auth/refresh')) {
      return next.handle(req);
    }
    // لو الـ Refresh Token منتهي → لازم يعمل Logout و يرجع على صفحة الـ Login
    if (status === AuthStatus.refreshTokenExpired) {
      this.authService.logout();

      this.router.navigate(['/auth/login']);
      return throwError(
        () => new Error('Session expired. Please login again.')
      );
    }

    // لو الـ Access Token شغال → نضيفه على الـ headers
    if (status === AuthStatus.valid) {
      const token = this.authService.getTokenFromLocal();
      if (token) {
        const cloned = req.clone({
          setHeaders: {
            Authorization: `Bearer ${token.token}`,
          },
        });
        return next.handle(cloned);
      }
    }

    // لو الـ Access Token منتهي بس الـ Refresh Token لسه عايش → نطلب توكن جديد
    if (status === AuthStatus.tokenExpired) {
      return this.authService.refreshToken().pipe(
        switchMap((newToken) => {
          const cloned = req.clone({
            setHeaders: {
              Authorization: `Bearer ${newToken.token}`,
            },
          });
          return next.handle(cloned);
        })
      );
    }

    // في أي حالة تانية → نكمل الطلب عادي
    return next.handle(req);
  }
}
