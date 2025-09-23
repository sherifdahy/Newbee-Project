import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IRegisterCompanyVm } from '../../../../core/view-models/responses/register-vm';
import { AuthService } from '../../../../core/services/backend/auth/auth.service';
import { Router } from '@angular/router';
import { ErrorMapperService } from '../../../../core/services/frontend/error-mapper/errormapper.service';
import { IApiErrorVm } from '../../../../core/view-models/responses/api-error-response';
import { passwordMatch } from '../../../../core/custome-validators/password-validator';
import { ValidatorPatterns } from '../../../../core/statics/validators-patterns';
import { ToastService } from '../../../../shared/services/toast.service';
import { UiAuthMessage } from '../../../../core/statics/ui-auth-messages';

@Component({
  selector: 'app-register',
  standalone: false,
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class RegisterComponent {
  userRegForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private auth: AuthService,
    private router: Router,
    private errorMapper: ErrorMapperService,
    private toast: ToastService
  ) {
    this.userRegForm = fb.group(
      {
        firstName: [
          '',
          [
            Validators.required,
            Validators.minLength(3),
            Validators.maxLength(100),
          ],
        ],
        lastName: [
          '',
          [
            Validators.required,
            Validators.minLength(3),
            Validators.maxLength(100),
          ],
        ],
        email: ['', [Validators.required, Validators.email]],
        password: [
          '',
          [
            Validators.required,
            Validators.pattern(ValidatorPatterns.StrongPassword),
          ],
        ],
        confirmPassword: ['', Validators.required],
        ssn: [
          '',
          [
            Validators.required,
            Validators.minLength(10),
            Validators.maxLength(20),
          ],
        ],
        name: [
          '',
          [
            Validators.required,
            Validators.minLength(3),
            Validators.maxLength(200),
          ],
        ],
        phoneNumber: [
          '',
          [Validators.required, Validators.pattern(/^\d{10,15}$/)],
        ],
        taxRegistrationNumber: [
          '',
          [
            Validators.required,
            // Validators.minLength(10),
            // Validators.maxLength(25),
          ],
        ],
      },
      { validators: passwordMatch() }
    );
  }

  submit() {
    const { confirmPassword, ...formValue } = this.userRegForm.value;
    const register: IRegisterCompanyVm = formValue as IRegisterCompanyVm;

    this.auth.registerCompany(register).subscribe({
      next: () => this.submitSuccess(register.email),
      error: (err: IApiErrorVm) => this.submitFail(err),
    });
  }

  private submitSuccess(email: string) {
    this.toast.success(UiAuthMessage.registerDataAdded);
    this.router.navigate(['/auth/otp', email]);
  }

  private submitFail(err: IApiErrorVm) {
    this.errorMapper.getBackEndErrors(this.userRegForm, err);
    if (this.errorMapper.getToastErrors.length > 0) {
      this.toast.errors(this.errorMapper.getToastErrors);
    }
  }

  get firstName() {
    return this.userRegForm.get('firstName');
  }
  get lastName() {
    return this.userRegForm.get('lastName');
  }
  get email() {
    return this.userRegForm.get('email');
  }
  get password() {
    return this.userRegForm.get('password');
  }
  get confirmPassword() {
    return this.userRegForm.get('confirmPassword');
  }
  get ssn() {
    return this.userRegForm.get('ssn');
  }
  get name() {
    return this.userRegForm.get('name');
  }
  get phoneNumber() {
    return this.userRegForm.get('phoneNumber');
  }
  get taxRegistrationNumber() {
    return this.userRegForm.get('taxRegistrationNumber');
  }
}
