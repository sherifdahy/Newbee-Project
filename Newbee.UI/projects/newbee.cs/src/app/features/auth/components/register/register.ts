import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IRegisterCompanyVm } from '../../../../core/view-models/responses/register-vm';
import { AuthService } from '../../../../core/services/backend/auth/auth.service';
import { ToastService } from '../../../../shared/services/toast.service';
import { Router } from '@angular/router';
import { ErrorMapperService } from '../../../../core/services/frontend/error-mapper/errormapper.service';
import { IApiErrorVm } from '../../../../core/view-models/responses/api-error-response';
import { passwordMatch } from '../../customevalidators/password-validator';
@Component({
  selector: 'app-register',
  standalone: false,
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {
  userRegForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private auth: AuthService,
    private toast: ToastService,
    private router: Router,
    private errorMapper: ErrorMapperService
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
            Validators.pattern(
              /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()\-=+{};:,<.>]).{8,}$/
            ),
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
            Validators.minLength(10),
            Validators.maxLength(25),
          ],
        ],
      },
      { validators: passwordMatch() }
    );
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

  submit() {
    const { confirmPassword, ...formValue } = this.userRegForm.value;

    let register: IRegisterCompanyVm = formValue as IRegisterCompanyVm;
    this.auth.registerCompany(register).subscribe({
      next: () => {
        this.toast.success('Data Added Successfully');
        this.router.navigate(['/auth/otp', register.email]);
      },
      error: (err: IApiErrorVm) => {
        if (err.errors) {
          let globalErrors: string[] = this.errorMapper.mapBackendErrors(
            this.userRegForm,
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
      },
    });
  }
}
