import { Injectable } from '@angular/core';
import {
  BehaviorSubject,
  catchError,
  Observable,
  retry,
  tap,
  throwError,
} from 'rxjs';
import { IRegisterCompanyVm } from '../view-models/register-vm';
import { environment } from '../../../environments/environment.prod';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { IApiErrorVm } from '../view-models/api-error-response';
import { IOtpVm } from '../view-models/otp-vm';
import { IOtpResendVm } from '../view-models/otp-resend-vm';
import { ILoginVm } from '../view-models/login-vm';
import { LocalStorgeService } from '../services/local-storge.service';
import { ILoginResponse } from '../view-models/login-response-vm';
import { ITokenStoreVm } from '../view-models/token-store-vm';
import { IRefreshTokenStoreVm } from '../view-models/refresh-token-store-vm';
import { AuthStatus } from '../enums/authstatus.enum';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private isUserLoginObservable: BehaviorSubject<AuthStatus>;
  public apiToken = 'token';
  public apiRefreshToken = 'refreshToken';
  private apiUrl: string = environment.apiBaseUrl;

  constructor(
    private http: HttpClient,
    private localStorage: LocalStorgeService
  ) {
    this.isUserLoginObservable = new BehaviorSubject<AuthStatus>(
      this.authStatus
    );
  }

  private handleError(error: HttpErrorResponse) {
    if (error.error && error.error.errors) {
      // رجع الـ IApiErrorVm زي ما هو
      return throwError(() => error.error as IApiErrorVm);
    }

    // في حالة errors تانية (500, network, إلخ)
    return throwError(
      () =>
        ({
          title: 'An unexpected error occurred, please try again',
          status: error.status,
          errors: {},
        } as IApiErrorVm)
    );
  }

  registerCompany(registerVm: IRegisterCompanyVm): Observable<void> {
    return this.http
      .post<void>(`${this.apiUrl}/Auth/register-company`, registerVm)
      .pipe(retry(2), catchError(this.handleError));
  }

  confirmEmail(email: string, code: string): Observable<void> {
    let otp: IOtpVm = {
      email: email,
      code: code,
    };
    return this.http
      .post<void>(`${this.apiUrl}/Auth/confirm-email`, otp)
      .pipe(retry(2), catchError(this.handleError));
  }

  reConfirmEmail(email: string): Observable<void> {
    let otp: IOtpResendVm = {
      email: email,
    };
    return this.http
      .post<void>(`${this.apiUrl}/Auth/resend-confirmation-email`, otp)
      .pipe(retry(2), catchError(this.handleError));
  }

  login(user: ILoginVm): Observable<ILoginResponse> {
    return this.http
      .post<ILoginResponse>(`${this.apiUrl}/Auth/login`, user)
      .pipe(
        retry(2),
        tap((res) => {
          let token: ITokenStoreVm = {
            token: res.token,
            expiresInSeconds: res.expiresIn,
            createdAt: new Date().toISOString(),
          };
          let refreshToken: IRefreshTokenStoreVm = {
            refreshToken: res.refreshToken,
            expiresAt: res.refreshTokenExpiration,
            createdAt: new Date().toISOString(),
          };
          this.localStorage.setItem<ITokenStoreVm>(this.apiToken, token);
          this.localStorage.setItem(this.apiRefreshToken, refreshToken);
          this.isUserLoginObservable.next(AuthStatus.Valid);
        }),
        catchError(this.handleError)
      );
  }

  logout() {
    this.localStorage.removeItem(this.apiToken);
    this.localStorage.removeItem(this.apiRefreshToken);
    this.isUserLoginObservable.next(AuthStatus.RefreshExpired);
  }

  get authStatus(): AuthStatus {
    let token = this.localStorage.getItem<ITokenStoreVm>(this.apiToken);
    let refreshToken = this.localStorage.getItem<IRefreshTokenStoreVm>(
      this.apiRefreshToken
    );

    if (!token || !refreshToken) return AuthStatus.RefreshExpired;

    const now = Date.now();
    const tokenCreated = new Date(token.createdAt).getTime();
    const tokenExpireAt = tokenCreated + token.expiresInSeconds * 1000;
    const isAccessExpired = now > tokenExpireAt;

    const refreshExpireAt = new Date(refreshToken.expiresAt).getTime();
    const isRefreshExpired = now > refreshExpireAt;

    if (isRefreshExpired) {
      return AuthStatus.RefreshExpired;
    }

    if (isAccessExpired) {
      return AuthStatus.AccessExpired;
    }

    return AuthStatus.Valid;
  }

  isUserLoginAsObservable() {
    return this.isUserLoginObservable.asObservable();
  }
}
