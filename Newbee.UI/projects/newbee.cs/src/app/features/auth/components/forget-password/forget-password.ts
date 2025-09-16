import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IForgetPassworVm } from '../../../../core/view-models/requests/forget-passwor-vm';
import { AuthService } from '../../../../core/services/backend/auth/auth.service';
import { ToastrService } from 'ngx-toastr';
import { IApiErrorVm } from '../../../../core/view-models/responses/api-error-response';
import { ErrorMapperService } from '../../../../core/services/frontend/error-mapper/errormapper.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-forget-password',
  standalone: false,
  templateUrl: './forget-password.html',
  styleUrl: './forget-password.css',
})
export class ForgetPassword {
  forgetPasswordForm: FormGroup;
  constructor(
    private fb: FormBuilder,
    private auth: AuthService,
    private toast: ToastrService,
    private errorMapper: ErrorMapperService,
    private router: Router
  ) {
    this.forgetPasswordForm = fb.group({
      email: ['', [Validators.required, Validators.email]],
    });
  }

  get email() {
    return this.forgetPasswordForm.get('email');
  }
  submit() {
    let forgetPassword: IForgetPassworVm = this.forgetPasswordForm
      .value as IForgetPassworVm;
    this.auth.forgetPassword(forgetPassword).subscribe(
      () => {
        this.toast.success('your email has sent correctly');
        this.router.navigate(['/auth/resetPassword', this.email?.value]);
      },
      (err: IApiErrorVm) => {
        if (err.errors) {
          let globalErrors: string[] = this.errorMapper.mapBackendErrors(
            this.forgetPasswordForm,
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
}
