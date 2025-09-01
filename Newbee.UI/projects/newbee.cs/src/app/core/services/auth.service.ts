import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable()
export class AuthService {

    private isUserLoginObservable:BehaviorSubject<boolean>;
    public apiToken='apiToken';
    constructor() {
      this.isUserLoginObservable=new BehaviorSubject<boolean>(this.isUserLogin);
     }

     //Regsiter ?
     //Login
     //Logout
     //gard Will Redirct To Login
     //An observable To Know To the other if it logged or not
     //I will Use the Inceptors to attach the token automatically when the req need it

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
