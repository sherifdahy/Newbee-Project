import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IForgetPassworVm } from '../../../../core/view-models/requests/forget-passwor-vm';
import { AuthService } from '../../../../core/services/backend/auth/auth.service';
import { IApiErrorVm } from '../../../../core/view-models/responses/api-error-response';
import { ErrorMapperService } from '../../../../core/services/frontend/error-mapper/errormapper.service';
import { Router } from '@angular/router';
import { ToastService } from '../../../../shared/services/toast.service';
import { UiAuthMessage } from '../../../../core/statics/ui-auth-messages';

@Component({
  selector: 'app-forget-password',
  standalone: false,
  templateUrl: './forget-password.html',
  styleUrl: './forget-password.css',
})
export class ForgetPasswordComponent {
  forgetPasswordForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private auth: AuthService,
    private errorMapper: ErrorMapperService,
    private toast: ToastService,
    private router: Router
  ) {
    this.forgetPasswordForm = fb.group({
      email: ['', [Validators.required, Validators.email]],
    });
  }

  submit() {
    const forgetPassword: IForgetPassworVm = this.forgetPasswordForm
      .value as IForgetPassworVm;

    this.auth.forgetPassword(forgetPassword).subscribe({
      next: () => this.submitSuccess(),
      error: (err: IApiErrorVm) => this.submitFail(err),
    });
  }

  private submitSuccess() {
    this.toast.success(UiAuthMessage.forgetPasswordResetEmail);
    this.router.navigate(['/auth/resetPassword', this.email?.value]);
  }

  private submitFail(err: IApiErrorVm) {
    this.errorMapper.getBackEndErrors(this.forgetPasswordForm, err);
    if (this.errorMapper.getToastErrors.length > 0) {
      this.toast.errors(this.errorMapper.getToastErrors);
    }
  }

  get email() {
    return this.forgetPasswordForm.get('email');
  }
}
