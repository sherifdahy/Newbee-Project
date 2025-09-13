import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, Observable, tap, throwError } from 'rxjs';
import { IRegisterCompanyVm } from '../../view-models/responses/register-vm';
import { environment } from '../../../../environments/environment.prod';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { IApiErrorVm } from '../../view-models/responses/api-error-response';
import { IOtpVm } from '../../view-models/requests/otp-vm';
import { IOtpResendVm } from '../../view-models/requests/otp-resend-vm';
import { ILoginVm } from '../../view-models/requests/login-vm';
import { LocalStorgeService } from '../local-storge/local-storge.service';
import { ILoginResponse } from '../../view-models/responses/login-response-vm';
import { ITokenStoreVm } from '../../view-models/stores/token-store-vm';
import { IRefreshTokenStoreVm } from '../../view-models/stores/refresh-token-store-vm';
import { AuthStatus } from '../../enums/authstatus.enum';
import { IRefreshRequestVm } from '../../view-models/requests/refresh-request-vm';
import { IUserVm } from '../../view-models/stores/user-vm';
import { IForgetPassworVm } from '../../view-models/requests/forget-passwor-vm';
import { IResetPasswordVm } from '../../view-models/requests/reset-password-vm';
import { StorageKeys } from '../../statics/storage-keys';
import { AuthMapper } from '../../mappers/auth-mapper';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private isUserLoginObservable: BehaviorSubject<AuthStatus>;
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
      return throwError(() => error.error as IApiErrorVm);
    }

    return throwError(
      () =>
        ({
          title: 'An unexpected error occurred, please try again',
          status: error.status,
          errors: {},
        } as IApiErrorVm)
    );
  }

  get authStatus(): AuthStatus {
    const token = this.getTokenFromLocal();
    const refreshToken = this.getRefreshTokenFromLocal();
    const now = Date.now();
    if (!token && !refreshToken) {
      return AuthStatus.emptyTokens;
    }
    if (!token && refreshToken) {
      return AuthStatus.refreshTokenExpired;
    }
    if (token && !refreshToken) {
      return this.isTokenExpired(now, token)
        ? AuthStatus.refreshTokenExpired
        : AuthStatus.valid;
    }
    if (token && refreshToken) {
      if (this.isRefreshTokenExpired(now, refreshToken)) {
        return AuthStatus.refreshTokenExpired;
      }

      if (this.isTokenExpired(now, token)) {
        return AuthStatus.tokenExpired;
      }

      return AuthStatus.valid;
    }
    return AuthStatus.emptyTokens;
  }

  registerCompany(registerVm: IRegisterCompanyVm): Observable<void> {
    return this.http
      .post<void>(`${this.apiUrl}/Auth/register-company`, registerVm)
      .pipe(catchError(this.handleError));
  }

  confirmEmail(otpVm: IOtpVm): Observable<void> {
    return this.http
      .post<void>(`${this.apiUrl}/Auth/confirm-email`, otpVm)
      .pipe(catchError(this.handleError));
  }

  reConfirmEmail(otpResendVm: IOtpResendVm): Observable<void> {
    return this.http
      .post<void>(`${this.apiUrl}/Auth/resend-confirmation-email`, otpResendVm)
      .pipe(catchError(this.handleError));
  }

  forgetPassword(forgetPassworVm: IForgetPassworVm): Observable<void> {
    return this.http
      .post<void>(`${this.apiUrl}/Auth/forget-password`, forgetPassworVm)
      .pipe(catchError(this.handleError));
  }

  resetPassword(resetPasswordVm: IResetPasswordVm): Observable<void> {
    return this.http
      .post<void>(`${this.apiUrl}/Auth/reset-password`, resetPasswordVm)
      .pipe(catchError(this.handleError));
  }

  login(user: ILoginVm): Observable<ILoginResponse> {
    return this.http
      .post<ILoginResponse>(`${this.apiUrl}/Auth/login`, user)
      .pipe(
        tap((res) => {
          this.saveTokenToLocal(res);
          this.saveRefreshTokenToLocal(res);
          this.saveUserInfoToLocal(res);
          this.isUserLoginObservable.next(AuthStatus.valid);
        }),
        catchError(this.handleError)
      );
  }

  logout() {
    this.localStorage.removeItem(StorageKeys.TOKEN);
    this.localStorage.removeItem(StorageKeys.REFRESH_TOKEN);
    this.localStorage.removeItem(StorageKeys.USER);
    this.isUserLoginObservable.next(AuthStatus.refreshTokenExpired);
  }

  refreshToken(): Observable<ILoginResponse> {
    const refreshRequest: IRefreshRequestVm = {
      token: this.getTokenFromLocal()?.token ?? '',
      refreshToken: this.getRefreshTokenFromLocal()?.refreshToken ?? '',
    };

    return this.http
      .post<ILoginResponse>(`${this.apiUrl}/Auth/refresh`, refreshRequest)
      .pipe(
        catchError(this.handleError),
        tap((res) => {
          this.saveTokenToLocal(res);
          this.saveRefreshTokenToLocal(res);
          this.isUserLoginObservable.next(AuthStatus.valid);
        })
      );
  }

  isTokenExpired(now: number, token: ITokenStoreVm): boolean {
    const tokenCreated = new Date(token.createdAt).getTime();
    const tokenExpireAt = tokenCreated + token.expiresInSeconds * 1000;
    return now > tokenExpireAt;
  }

  isRefreshTokenExpired(
    now: number,
    refreshToken: IRefreshTokenStoreVm
  ): boolean {
    const refreshExpireAt = new Date(refreshToken.expiresAt).getTime();
    return now > refreshExpireAt;
  }

  isUserLoginAsObservable() {
    return this.isUserLoginObservable.asObservable();
  }

  getTokenFromLocal(): ITokenStoreVm | null {
    return this.localStorage.getItem<ITokenStoreVm>(StorageKeys.TOKEN);
  }

  getRefreshTokenFromLocal(): IRefreshTokenStoreVm | null {
    return this.localStorage.getItem<IRefreshTokenStoreVm>(
      StorageKeys.REFRESH_TOKEN
    );
  }

  saveTokenToLocal(res: ILoginResponse) {
    const token = AuthMapper.mapToTokenStore(res);
    this.localStorage.setItem<ITokenStoreVm>(StorageKeys.TOKEN, token);
  }

  saveRefreshTokenToLocal(res: ILoginResponse) {
    const refreshToken = AuthMapper.mapToRefreshTokenStore(res);
    this.localStorage.setItem<IRefreshTokenStoreVm>(
      StorageKeys.REFRESH_TOKEN,
      refreshToken
    );
  }

  saveUserInfoToLocal(res: ILoginResponse) {
    const user = AuthMapper.mapToUserStore(res);
    this.localStorage.setItem<IUserVm>(StorageKeys.USER, user);
  }
}
