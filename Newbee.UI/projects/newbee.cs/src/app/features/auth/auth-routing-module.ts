import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Login } from './components/login/login';
import { Logout } from './components/logout/logout';
import { Register } from './components/register/register';
import { Otp } from './components/otp/otp';

const routes: Routes = [
  {path:'',redirectTo:'register',pathMatch:'full'},
  {path:'register',component:Register},
  {path:'login',component:Login},
  {path:'logout',component:Logout},
  {path:'otp/:email',component:Otp}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthRoutingModule { }
