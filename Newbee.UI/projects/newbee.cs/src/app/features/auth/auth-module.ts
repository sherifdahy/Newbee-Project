import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared-module';
import { AuthRoutingModule } from './auth-routing-module';
import { Login } from './components/login/login';
import { Register } from './components/register/register';
import { ReactiveFormsModule } from '@angular/forms';
import { Otp } from './components/otp/otp';

@NgModule({
  declarations: [Login, Register, Otp],
  imports: [CommonModule, AuthRoutingModule, SharedModule, ReactiveFormsModule],
})
export class AuthModule {}
