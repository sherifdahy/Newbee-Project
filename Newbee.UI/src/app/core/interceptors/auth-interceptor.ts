import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { catchError, Observable, switchMap, throwError } from 'rxjs';
import { Router } from '@angular/router';

@Injectable()
export class AuthTokenInterceptor implements HttpInterceptor {
  private authService = inject(AuthService);
  private router = inject(Router);

  // ✅ الـ URLs اللي ما نحطش عليها token
  private readonly SKIP_TOKEN_URLS = [
    '/api/auth/get-token',
    '/api/auth/refresh',
    '/api/auth/register'
  ];

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // ✅ لو الريكويست في الـ skip list، ما نحطش token
    if (this.shouldSkipToken(req.url)) {
      return next.handle(req);
    }

    // ✅ إضافة الـ token للريكويست
    const modifiedRequest = this.addToken(req);

    return next.handle(modifiedRequest).pipe(
      catchError((error: HttpErrorResponse) => {
        // ✅ لو 401 وما نفعش الريفريش
        if (error.status === 401 && !this.shouldSkipToken(req.url)) {
          return this.handle401Error(req, next);
        }

        return throwError(() => error);
      })
    );
  }

  /**
   * إضافة الـ token للريكويست
   */
  private addToken(req: HttpRequest<any>): HttpRequest<any> {
    const token = this.authService.getToken();

    if (token) {
      return req.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`
        }
      });
    }

    return req;
  }

  /**
   * التعامل مع 401 error
   */
  private handle401Error(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    console.log('🔄 Token expired, trying to refresh...');

    return this.authService.refreshToken().pipe(
      switchMap(() => {
        console.log('✅ Token refreshed, retrying request...');
        // إعادة المحاولة بالتوكن الجديد
        const newRequest = this.addToken(req);
        return next.handle(newRequest);
      }),
      catchError((refreshError) => {
        console.error('❌ Refresh failed, logging out...');
        this.authService.logout();
        this.router.navigate(['/auth/login']);
        return throwError(() => refreshError);
      })
    );
  }

  /**
   * التحقق من الـ URLs اللي ما نحطش عليها token
   */
  private shouldSkipToken(url: string): boolean {
    return this.SKIP_TOKEN_URLS.some(skipUrl => url.includes(skipUrl));
  }
}
