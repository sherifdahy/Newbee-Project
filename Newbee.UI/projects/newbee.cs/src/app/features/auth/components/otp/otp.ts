import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { AuthService } from '../../../../core/services/auth/auth.service';
import { ToastService } from '../../../../shared/services/toast.service';
import { IApiErrorVm } from '../../../../core/view-models/responses/api-error-response';
import { ErrorMapperService } from '../../../../core/services/error-mapper/errormapper.service';
import { IOtpVm } from '../../../../core/view-models/requests/otp-vm';
import { IOtpResendVm } from '../../../../core/view-models/requests/otp-resend-vm';

@Component({
  selector: 'app-otp',
  standalone: false,
  templateUrl: './otp.html',
  styleUrl: './otp.css',
})
export class Otp implements OnInit {
  otpFrom: FormGroup;
  email: string = '';
  constructor(
    private toast: ToastService,
    private fb: FormBuilder,
    private activeRouter: ActivatedRoute,
    private router: Router,
    private auth: AuthService,
    private errorMapper: ErrorMapperService
  ) {
    this.otpFrom = fb.group({
      code: [
        '',
        [Validators.required, Validators.minLength(6), Validators.maxLength(6)],
      ],
    });
  }
  ngOnInit(): void {
    this.activeRouter.paramMap.subscribe((params) => {
      this.email = params.get('email')!;
    });
  }

  get code() {
    return this.otpFrom.get('code');
  }

  submit() {
    let otp: IOtpVm = this.otpFrom.value as IOtpVm;
    otp.email = this.email;
    this.auth.confirmEmail(otp).subscribe(
      () => {
        this.toast.success('Correct Code');
        this.router.navigate(['/auth/login']);
      },
      (err: IApiErrorVm) => {
        if (err.errors) {
          let globalErrors: string[] = this.errorMapper.mapBackendErrors(
            this.otpFrom,
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

  resendOtp() {
    let otp: IOtpResendVm = {
      email: this.email,
    };
    otp.email = this.email;
    this.auth.reConfirmEmail(otp).subscribe(
      () => {
        this.toast.success('Resend In the Way');
      },
      (error: any) => {
        this.toast.error(error);
      }
    );
  }
}
