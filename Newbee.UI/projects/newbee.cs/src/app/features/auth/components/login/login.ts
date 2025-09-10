import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ILoginVm } from '../../../../core/view-models/requests/login-vm';
import { AuthService } from '../../../../core/services/auth/auth.service';
import { Router } from '@angular/router';
import { IApiErrorVm } from '../../../../core/view-models/responses/api-error-response';
import { ErrorMapperService } from '../../../../core/services/error-mapper/errormapper.service';

@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  userLoginForm: FormGroup;

  constructor(
    private toast: ToastrService,
    private fb: FormBuilder,
    private auth: AuthService,
    private router: Router,
    private errorMapper: ErrorMapperService
  ) {
    this.userLoginForm = fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: [
        '',
        [
          Validators.required,
          Validators.pattern(
            /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()\-=+{};:,<.>]).{8,}$/
          ),
        ],
      ],
    });
  }

  submit() {
    let user: ILoginVm = this.userLoginForm.value as ILoginVm;
    this.auth.login(user).subscribe(
      () => {
        this.toast.success('Login success');
        this.router.navigate(['/home/welcome']);
      },
      (err: IApiErrorVm) => {
        if (err.errors) {
          let globalErrors: string[] = this.errorMapper.mapBackendErrors(
            this.userLoginForm,
            err.errors
          );
          if (globalErrors.length > 0) {
            globalErrors.forEach((global) => {
              this.toast.error(global);
            });
          }
        } else {
          this.toast.error(err.title);
        }
      }
    );
  }

  get email() {
    return this.userLoginForm.get('email');
  }

  get password() {
    return this.userLoginForm.get('password');
  }
}
