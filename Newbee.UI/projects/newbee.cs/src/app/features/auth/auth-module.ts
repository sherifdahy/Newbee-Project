import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthRoutingModule } from './auth-routing-module';
import {Login} from './components/login/login';
import {Logout} from './components/logout/logout';

@NgModule({
  declarations: [Login,Logout],
  imports: [
    CommonModule,
    AuthRoutingModule
  ]
})
export class AuthModule { }
