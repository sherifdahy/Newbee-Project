import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ILoginVm } from '../../../../core/view-models/requests/login-vm';
import { AuthService } from '../../../../core/services/backend/auth/auth.service';
import { Router } from '@angular/router';
import { IApiErrorVm } from '../../../../core/view-models/responses/api-error-response';
import { ErrorMapperService } from '../../../../core/services/frontend/error-mapper/errormapper.service';
import { ValidatorPatterns } from '../../../../core/statics/validators-patterns';
import { ToastService } from '../../../../shared/services/toast.service';
import { UiAuthMessage } from '../../../../core/statics/ui-auth-messages';
@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class LoginComponent {
  userLoginForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private auth: AuthService,
    private router: Router,
    private errorMapper: ErrorMapperService,
    private toast: ToastService
  ) {
    this.userLoginForm = fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: [
        '',
        [
          Validators.required,
          Validators.pattern(ValidatorPatterns.StrongPassword),
        ],
      ],
    });
  }

  submit() {
    let user: ILoginVm = this.userLoginForm.value as ILoginVm;
    this.auth.login(user).subscribe({
      next: () => this.submitSuccess(),
      error: (err: IApiErrorVm) => this.submitFail(err),
    });
  }

  private submitSuccess() {
    this.toast.success(UiAuthMessage.loginSuccess);
    this.router.navigate(['/home/welcome']);
  }
  private submitFail(err: IApiErrorVm) {
    this.errorMapper.getBackEndErrors(this.userLoginForm, err);
    if (this.errorMapper.getToastErrors.length > 0) {
      this.toast.errors(this.errorMapper.getToastErrors);
    }
  }

  get email() {
    return this.userLoginForm.get('email');
  }

  get password() {
    return this.userLoginForm.get('password');
  }
}
