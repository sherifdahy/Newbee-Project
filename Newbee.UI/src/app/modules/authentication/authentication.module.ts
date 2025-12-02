import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { LoginFormComponent } from './components/login-form/login-form.component';
import { ClientRegisterFormComponent } from './components/client-register-form/client-register-form.component';
import { PartnerRegisterFormComponent } from './components/partner-register-form/partner-register-form.component';
import { AuthLayoutComponent } from '../../shared/layouts/auth-layout/auth-layout.component';

const routes: Routes = [
  {
    path: '',
    component: AuthLayoutComponent,
    children: [
      {
        path: 'login',
        component: LoginFormComponent,
      },
      {
        path: 'partner',
        children: [
          {
            path: 'register',
            component: PartnerRegisterFormComponent
          }
        ]
      },
      {
        path: 'client',
        children: [
          {
            path: 'register',
            component: ClientRegisterFormComponent
          }
        ]
      }
    ]
  }
]

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    RouterModule.forChild(routes),
  ],
  declarations: [
    LoginFormComponent,
    ClientRegisterFormComponent,
    PartnerRegisterFormComponent,
    AuthLayoutComponent,
  ]
})
export class AuthenticationModule { }
