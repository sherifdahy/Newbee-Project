import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../../../core/services/backend/auth/auth.service';
import { IApiErrorVm } from '../../../../core/view-models/responses/api-error-response';
import { ErrorMapperService } from '../../../../core/services/frontend/error-mapper/errormapper.service';
import { IOtpVm } from '../../../../core/view-models/requests/otp-vm';
import { IOtpResendVm } from '../../../../core/view-models/requests/otp-resend-vm';
import { ToastService } from '../../../../shared/services/toast.service';
import { UiAuthMessage } from '../../../../core/statics/ui-auth-messages';

@Component({
  selector: 'app-otp',
  standalone: false,
  templateUrl: './otp.html',
  styleUrl: './otp.css',
})
export class OtpComponent implements OnInit {
  otpForm: FormGroup;
  email: string = '';

  constructor(
    private fb: FormBuilder,
    private activeRouter: ActivatedRoute,
    private router: Router,
    private auth: AuthService,
    private errorMapper: ErrorMapperService,
    private toast: ToastService
  ) {
    this.otpForm = fb.group({
      code: [
        '',
        [Validators.required, Validators.minLength(6), Validators.maxLength(6)],
      ],
    });
  }

  ngOnInit(): void {
    this.email = this.activeRouter.snapshot.paramMap.get('email') ?? '';
  }

  submit() {
    const otp: IOtpVm = { ...this.otpForm.value, email: this.email };
    this.auth.confirmEmail(otp).subscribe({
      next: () => this.submitSuccess(),
      error: (err: IApiErrorVm) => this.submitFail(err),
    });
  }

  resendOtp() {
    const otp: IOtpResendVm = { email: this.email };

    this.auth.reConfirmEmail(otp).subscribe({
      next: () => this.toast.success(UiAuthMessage.otpResendInProgress),
      error: (error: any) => this.toast.error(error),
    });
  }

  private submitSuccess() {
    this.toast.success(UiAuthMessage.otpCorrectCode);
    this.router.navigate(['/auth/login']);
  }

  private submitFail(err: IApiErrorVm) {
    this.errorMapper.getBackEndErrors(this.otpForm, err);
    if (this.errorMapper.getToastErrors.length > 0) {
      this.toast.errors(this.errorMapper.getToastErrors);
    }
  }

  get code() {
    return this.otpForm.get('code');
  }
}
