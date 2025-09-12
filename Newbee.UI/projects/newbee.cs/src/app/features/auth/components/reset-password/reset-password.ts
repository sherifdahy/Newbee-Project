import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IResetPasswordVm } from '../../../../core/view-models/requests/reset-password-vm';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../../../core/services/auth/auth.service';
import { ToastrService } from 'ngx-toastr';
import { IApiErrorVm } from '../../../../core/view-models/responses/api-error-response';
import { ErrorMapperService } from '../../../../core/services/error-mapper/errormapper.service';

@Component({
  selector: 'app-reset-password',
  standalone: false,
  templateUrl: './reset-password.html',
  styleUrl: './reset-password.css',
})
export class ResetPassword implements OnInit {
  resetPasswordForm: FormGroup;
  email: string;
  constructor(
    private fb: FormBuilder,
    private activeRouter: ActivatedRoute,
    private auth: AuthService,
    private toast: ToastrService,
    private router: Router,
    private errorMapper: ErrorMapperService
  ) {
    this.email = '';
    this.resetPasswordForm = this.fb.group({
      code: [
        '',
        [Validators.required, Validators.minLength(6), Validators.maxLength(6)],
      ],
      newPassword: [
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
  ngOnInit(): void {
    this.activeRouter.paramMap.subscribe((params) => {
      this.email = params.get('email')!;
    });
  }
  get code() {
    return this.resetPasswordForm.get('code');
  }
  get newPassword() {
    return this.resetPasswordForm.get('newPassword');
  }
  submit() {
    let resetPasswordForm: IResetPasswordVm = this.resetPasswordForm
      .value as IResetPasswordVm;
    resetPasswordForm.email = this.email;
    this.auth.resetPassword(resetPasswordForm).subscribe(
      () => {
        this.toast.success('Your New Password Has been reset correctly');
        this.router.navigate(['/auth/login']);
      },
      (err: IApiErrorVm) => {
        if (err.errors) {
          let globalErrors: string[] = this.errorMapper.mapBackendErrors(
            this.resetPasswordForm,
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
