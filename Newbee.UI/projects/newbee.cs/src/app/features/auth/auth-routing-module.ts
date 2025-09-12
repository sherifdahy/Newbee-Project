import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Login } from './components/login/login';
import { Register } from './components/register/register';
import { Otp } from './components/otp/otp';
import { ForgetPassword } from './components/forget-password/forget-password';
import { ResetPassword } from './components/reset-password/reset-password';

const routes: Routes = [
  { path: '', redirectTo: 'register', pathMatch: 'full' },
  { path: 'register', component: Register },
  { path: 'login', component: Login },
  { path: 'forgetPassword', component: ForgetPassword },
  { path: 'resetPassword/:email', component: ResetPassword },
  { path: 'otp/:email', component: Otp },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AuthRoutingModule {}
