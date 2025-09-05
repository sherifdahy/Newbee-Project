import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, Observable, retry, throwError } from 'rxjs';
import { IRegisterCompanyVm } from '../view-models/register-vm';
import {environment} from '../../../environments/environment.prod';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { IApiErrorVm } from '../view-models/api-error-response';
import { IOtpVm } from '../view-models/otp-vm';
@Injectable()
export class AuthService {

    private isUserLoginObservable:BehaviorSubject<boolean>;
    public apiToken='apiToken';
    private apiUrl:string = environment.apiBaseUrl;
    constructor(private http:HttpClient) {
      this.isUserLoginObservable=new BehaviorSubject<boolean>(this.isUserLogin);
     }
private handleError(error: HttpErrorResponse) {
  let errorMessage = 'An unexpected error occurred, please try again';
  if (error.error) {
    const apiError = error.error as IApiErrorVm;

    if (apiError.errors && apiError.errors.length > 0) {
      errorMessage = apiError.errors.join(', ');
    }
  }

  return throwError(() => new Error(errorMessage));
}
     registerCompany(registerVm:IRegisterCompanyVm):Observable<void>{
       return this.http.post<void>(`${this.apiUrl}/Auth/register-company`,registerVm).pipe(
          retry(2),
          catchError(this.handleError)
        )
     }
     confirmEmail(email:string,code:string):Observable<void>{
        let otp: IOtpVm = {
          email: email,
          code: code
        };
          return this.http.post<void>(`${this.apiUrl}/Auth/confirm-email`,otp).pipe(
          retry(2),
          catchError(this.handleError)
        )
     }
     login(userName:string,password:string){
      //For Now Will Be static
      let token:string="123";
      localStorage.setItem(this.apiToken,token);
      this.isUserLoginObservable.next(true);
     }

     logout(){
      localStorage.removeItem(this.apiToken);
      this.isUserLoginObservable.next(false);
     }

     get isUserLogin(){
      return localStorage.getItem(this.apiToken)?true:false;
     }
     isUserLoginAsObservable(){
        return this.isUserLoginObservable.asObservable();
     }
     getToken(){
      return localStorage.getItem(this.apiToken);
     }


}
