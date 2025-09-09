import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpInterceptorFn,
  HttpRequest,
} from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { environment } from '../../../environments/environment';
import { Router } from '@angular/router';
import { Observable, throwError, catchError, switchMap } from 'rxjs';
import { AuthStatus } from '../enums/authstatus.enum';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService, private router: Router) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const status = this.authService.authStatus;

    if (status === AuthStatus.emptyTokens) {
      alert('Start');
      return next.handle(req);
    }

    if (req.url.includes('/Auth/refresh')) {
      return next.handle(req);
    }
    // لو الـ Refresh Token منتهي → لازم يعمل Logout و يرجع على صفحة الـ Login
    if (status === AuthStatus.refreshTokenExpired) {
      this.authService.logout();
      console.log('refreshTokenExpired');
      alert('refreshTokenExpired');

      this.router.navigate(['/auth/login']);
      return throwError(
        () => new Error('Session expired. Please login again.')
      );
    }

    // لو الـ Access Token شغال → نضيفه على الـ headers
    if (status === AuthStatus.valid) {
      const token = this.authService.getTokenFromLocal();
      console.log('Valid');
      alert('Valid');
      if (token) {
        const cloned = req.clone({
          setHeaders: {
            Authorization: `Bearer ${token.token}`,
          },
        });
        return next.handle(cloned);
        // .pipe(catchError((err) => this.handleAuthError(err)));
      }
    }

    // لو الـ Access Token منتهي بس الـ Refresh Token لسه عايش → نطلب توكن جديد
    if (status === AuthStatus.tokenExpired) {
      console.log('tokenExpired');
      alert('tokenExpired');

      return this.authService.refreshToken().pipe(
        switchMap((newToken) => {
          const cloned = req.clone({
            setHeaders: {
              Authorization: `Bearer ${newToken.token}`,
            },
          });
          return next.handle(cloned);
        })
        // catchError((err) => {
        //   this.authService.logout();
        //   this.router.navigate(['/login']);
        //   return throwError(() => err);
        // })
      );
    }

    // في أي حالة تانية → نكمل الطلب عادي
    return next.handle(req);
  }

  // private handleAuthError(err: any) {
  //   alert('handleAuthError');
  //   if (err instanceof HttpErrorResponse && err.status === 401) {
  //     this.authService.logout();
  //     this.router.navigate(['/login']);
  //   }
  //   return throwError(() => err);
  // }
}
