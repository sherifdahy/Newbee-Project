import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NotificationService } from '../../../../core/services/notification.service';
import { AuthService } from '../../../../core/services/auth.service';
import { CompanyRequest } from '../../../../core/models/company/requests/company-request';
import { RegisterCompanyRequest } from '../../../../core/models/authentication/requests/register-company-request';
import { Router } from '@angular/router';

@Component({
  selector: 'app-partner-register-form',
  standalone: false,
  templateUrl: './partner-register-form.component.html',
  styleUrls: ['./partner-register-form.component.css']
})
export class PartnerRegisterFormComponent implements OnInit {

  registerPartnerForm!: FormGroup;

  constructor(
    private router: Router,
    private fb: FormBuilder,
    private notificationService: NotificationService,
    private authService: AuthService,
  ) { }

  ngOnInit() {
    this.registerPartnerForm = this.fb.group({
      name: ['', Validators.required],
      rin: ['', [Validators.required,Validators.minLength(9)]],
      manager: this.fb.group({
        name: ['', Validators.required],
        identifierNumber: [''],
        phoneNumber: [''],
        email: ['', [Validators.required,Validators.email]],
        password: ['', Validators.required],
      })
    });
  }



  onSubmit() {
    if (this.registerPartnerForm.invalid) {
      this.registerPartnerForm.markAllAsTouched();
      return;
    }

    let companyRequest = this.registerPartnerForm.value as RegisterCompanyRequest;

    this.authService.registerCompany(companyRequest).subscribe({
      next: () => {
        this.router.navigate(['auth/login']);
      },
      error: (errors) => {
        this.notificationService.showError(errors);
      }
    })
  }

}
