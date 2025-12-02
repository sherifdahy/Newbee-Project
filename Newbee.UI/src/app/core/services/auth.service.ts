import { Injectable } from '@angular/core';
import { ApiCallService } from './api-call.service';
import { AuthResponse } from '../models/authentication/responses/auth-response';
import { BehaviorSubject, catchError, Observable, tap, throwError } from 'rxjs';
import { AuthenticatedUserResponse } from '../models/authentication/responses/authenticated-user-response';
import { RegisterCompanyRequest } from '../models/authentication/requests/register-company-request';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly STORAGE_KEY = 'auth-obj';
  private userSubject: BehaviorSubject<AuthenticatedUserResponse | null>;
  private isLoggedInSubject: BehaviorSubject<boolean>;
  private isRefreshing = false; // ✅ منع multiple refresh calls

  constructor(private apiCall: ApiCallService) {
    this.userSubject = new BehaviorSubject<AuthenticatedUserResponse | null>(null);
    this.isLoggedInSubject = new BehaviorSubject<boolean>(false);
    this.loadUserFromToken();
  }

  /**
   * Login and store tokens
   */
  login(email: string, password: string): Observable<AuthResponse> {
    return this.apiCall.post<AuthResponse>('api/auth/get-token', {
      email,
      password
    }).pipe(
      tap(response => {
        this.saveTokens(response);
        this.loadUserFromToken();
      }),
      catchError((response) => {
        return throwError(() => response.error?.errors);
      })
    );
  }

  registerCompany(request : RegisterCompanyRequest) : Observable<void>
  {
    return this.apiCall.post<void>('api/auth/register-company',request).pipe(
      catchError((response)=>{
        return throwError(()=> response.error?.errors);
      })
    )
  }

  /**
   * Logout and clear everything
   */
  logout(): void {
    localStorage.removeItem(this.STORAGE_KEY);
    this.userSubject.next(null);
    this.isLoggedInSubject.next(false);
    this.isRefreshing = false;
  }

  /**
   * Refresh the access token
   */
  refreshToken(): Observable<AuthResponse> {
    // ✅ لو في refresh جاري، استنى
    if (this.isRefreshing) {
      return throwError(() => new Error('Refresh already in progress'));
    }

    const tokens = this.getStoredTokens();

    if (!tokens?.refreshToken) {
      this.logout();
      return throwError(() => new Error('No refresh token found'));
    }

    this.isRefreshing = true;

    // ✅ إرسال الـ refreshToken فقط (حسب الـ API بتاعك)
    return this.apiCall.post<AuthResponse>('api/auth/refresh', {
      refreshToken: tokens.refreshToken
    }).pipe(
      tap((response: AuthResponse) => {
        console.log('✅ Token refreshed successfully');
        this.saveTokens(response); // حفظ التوكنات الجديدة
        this.loadUserFromToken(); // ✅ تحديث الـ user والحالة
        this.isRefreshing = false;
      }),
      catchError((error) => {
        console.error('❌ Refresh token failed:', error);
        this.isRefreshing = false;
        this.logout();
        return throwError(() => error);
      })
    );
  }

  /**
   * Load user from token and update subjects
   */
  private loadUserFromToken(): void {
    const authObj = this.getStoredTokens();

    if (!authObj?.token) {
      this.logout();
      return;
    }

    try {
      const payload = this.decodeToken(authObj.token);
      const user: AuthenticatedUserResponse = {
        id: payload.sub,
        email: payload.email,
        roles: payload.roles || [],
        permissions: payload.permissions || []
      };

      this.userSubject.next(user);
      this.isLoggedInSubject.next(true);
    } catch (error) {
      console.error('❌ Invalid token:', error);
      this.logout();
    }
  }

  private decodeToken(token: string): any {
    try {
      const payload = token.split('.')[1];
      return JSON.parse(atob(payload));
    } catch (error) {
      throw new Error('Invalid token format');
    }
  }

  /**
   * Get the current access token
   */
  getToken(): string | null {
    const tokens = this.getStoredTokens();
    return tokens?.token || null;
  }

  /**
   * Get the refresh token
   */
  getRefreshToken(): string | null {
    const tokens = this.getStoredTokens();
    return tokens?.refreshToken || null;
  }

  /**
   * Get stored tokens from localStorage
   */
  private getStoredTokens(): AuthResponse | null {
    try {
      const item = localStorage.getItem(this.STORAGE_KEY);
      return item ? JSON.parse(item) : null;
    } catch (error) {
      console.error('Error reading tokens from localStorage:', error);
      return null;
    }
  }

  /**
   * Save tokens to localStorage
   */
  private saveTokens(response: AuthResponse): void {
    try {
      localStorage.setItem(this.STORAGE_KEY, JSON.stringify(response));
    } catch (error) {
      console.error('Error saving tokens to localStorage:', error);
    }
  }

  /**
   * Check if token is expired
   */
  isTokenExpired(): boolean {
    const token = this.getToken();
    if (!token) return true;

    try {
      const payload = this.decodeToken(token);
      const exp = payload.exp * 1000; // Convert to milliseconds
      return Date.now() >= exp;
    } catch {
      return true;
    }
  }

  /**
   * Get current user (synchronous)
   */
  get currentUser(): AuthenticatedUserResponse | null {
    return this.userSubject.value;
  }

  /**
   * Observable for user changes
   */
  get currentUser$(): Observable<AuthenticatedUserResponse | null> {
    return this.userSubject.asObservable();
  }

  /**
   * Observable for login status
   */
  get isLoggedIn$(): Observable<boolean> {
    return this.isLoggedInSubject.asObservable();
  }

  /**
   * Check login status (synchronous)
   */
  get isLoggedIn(): boolean {
    return this.isLoggedInSubject.value;
  }
}
