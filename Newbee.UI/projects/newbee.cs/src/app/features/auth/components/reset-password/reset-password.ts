import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IResetPasswordVm } from '../../../../core/view-models/requests/reset-password-vm';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../../../core/services/backend/auth/auth.service';
import { IApiErrorVm } from '../../../../core/view-models/responses/api-error-response';
import { ErrorMapperService } from '../../../../core/services/frontend/error-mapper/errormapper.service';
import { IForgetPassworVm } from '../../../../core/view-models/requests/forget-passwor-vm';
import { passwordMatch } from '../../../../core/custome-validators/password-validator';
import { ValidatorPatterns } from '../../../../core/statics/validators-patterns';
import { ToastService } from '../../../../shared/services/toast.service';
import { UiAuthMessage } from '../../../../core/statics/ui-auth-messages';

@Component({
  selector: 'app-reset-password',
  standalone: false,
  templateUrl: './reset-password.html',
  styleUrl: './reset-password.css',
})
export class ResetPasswordComponent implements OnInit {
  resetPasswordForm: FormGroup;
  email: string = '';

  constructor(
    private fb: FormBuilder,
    private activeRouter: ActivatedRoute,
    private auth: AuthService,
    private router: Router,
    private errorMapper: ErrorMapperService,
    private toast: ToastService
  ) {
    this.resetPasswordForm = this.fb.group(
      {
        code: [
          '',
          [
            Validators.required,
            Validators.minLength(6),
            Validators.maxLength(6),
          ],
        ],
        newPassword: [
          '',
          [
            Validators.required,
            Validators.pattern(ValidatorPatterns.StrongPassword),
          ],
        ],
        confirmNewPassword: ['', Validators.required],
      },
      { validators: passwordMatch('newPassword', 'confirmNewPassword') }
    );
  }

  ngOnInit(): void {
    this.email = this.activeRouter.snapshot.paramMap.get('email') ?? '';
  }

  submit() {
    const { confirmNewPassword, ...formValue } = this.resetPasswordForm.value;
    const resetPassword: IResetPasswordVm = { ...formValue, email: this.email };

    this.auth.resetPassword(resetPassword).subscribe({
      next: () => this.submitSuccess(),
      error: (err: IApiErrorVm) => this.submitFail(err),
    });
  }
  resendCode() {
    const forgetPassword: IForgetPassworVm = { email: this.email };

    this.auth.forgetPassword(forgetPassword).subscribe({
      next: () => this.toast.success(UiAuthMessage.resetPasswordCodeReset),
      error: (err: IApiErrorVm) => this.submitFail(err),
    });
  }

  private submitSuccess() {
    this.toast.success(UiAuthMessage.resetPasswordResetSuccess);
    this.router.navigate(['/auth/login']);
  }

  private submitFail(err: IApiErrorVm) {
    this.errorMapper.getBackEndErrors(this.resetPasswordForm, err);
    if (this.errorMapper.getToastErrors.length > 0) {
      this.toast.errors(this.errorMapper.getToastErrors);
    }
  }

  get code() {
    return this.resetPasswordForm.get('code');
  }
  get newPassword() {
    return this.resetPasswordForm.get('newPassword');
  }
  get confirmNewPassword() {
    return this.resetPasswordForm.get('confirmNewPassword');
  }
}
