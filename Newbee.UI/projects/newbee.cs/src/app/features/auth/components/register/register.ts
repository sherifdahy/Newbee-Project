import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IRegisterCompanyVm } from '../../../../core/view-models/register-vm';
import { AuthService } from '../../../../core/services/auth.service';
import { ToastService } from '../../../../shared/services/toast.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: false,
  templateUrl: './register.html',
  styleUrl: './register.css'
})
export class Register {
  userRegForm: FormGroup;

  constructor(private fb: FormBuilder, private auth: AuthService,private toast:ToastService,private router:Router) {
    this.userRegForm = fb.group({
      name: ['', [Validators.required]],
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      ssn: ['', [Validators.required]],
      password: ['', [Validators.required, Validators.minLength(8)]],
      phoneNumber: ['', [Validators.required]],
      taxRegistrationNumber: ['', [Validators.required]],
    });
  }

  get name() { return this.userRegForm.get('name'); }
  get firstName() { return this.userRegForm.get('firstName'); }
  get lastName() { return this.userRegForm.get('lastName'); }
  get email() { return this.userRegForm.get('email'); }
  get ssn() { return this.userRegForm.get('ssn'); }
  get password() { return this.userRegForm.get('password'); }
  get phoneNumber() { return this.userRegForm.get('phoneNumber'); }
  get taxRegistrationNumber() { return this.userRegForm.get('taxRegistrationNumber'); }

submit() {
  let register: IRegisterCompanyVm = this.userRegForm.value as IRegisterCompanyVm;

  this.auth.registerCompany(register).subscribe(
    () => {
      this.toast.success("Data Added Successfully");this.router.navigate(['/auth/otp', register.email]);
    },
    (error) => { this.toast.error(error) }
  );
}
}
