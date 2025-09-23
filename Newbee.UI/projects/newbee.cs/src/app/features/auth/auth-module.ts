import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared-module';
import { AuthRoutingModule } from './auth-routing-module';
import { LoginComponent } from './components/login/login';
import { RegisterComponent } from './components/register/register';
import { ReactiveFormsModule } from '@angular/forms';
import { OtpComponent } from './components/otp/otp';
import { ForgetPasswordComponent } from './components/forget-password/forget-password';
import { ResetPasswordComponent } from './components/reset-password/reset-password';

@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent,
    OtpComponent,
    ForgetPasswordComponent,
    ResetPasswordComponent,
  ],
  imports: [CommonModule, AuthRoutingModule, SharedModule, ReactiveFormsModule],
})
export class AuthModule {}
