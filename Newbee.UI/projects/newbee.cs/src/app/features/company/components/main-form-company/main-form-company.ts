import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ICompany } from '../../../../core/models/company';

import { CompanyService } from '../../../../core/services/backend/company/company.service';
import { ActivatedRoute, Router } from '@angular/router';
import { IApiErrorVm } from '../../../../core/view-models/responses/api-error-response';
import { ErrorMapperService } from '../../../../core/services/frontend/error-mapper/errormapper.service';
import { ToastService } from '../../../../shared/services/toast.service';
@Component({
  selector: 'app-main-form-company',
  standalone: false,
  templateUrl: './main-form-company.html',
  styleUrl: './main-form-company.css',
})
export class MainFormCompanyComponent implements OnInit {
  mainForm: FormGroup;
  id: number = 0;
  company: ICompany = {} as ICompany;
  constructor(
    private fb: FormBuilder,
    private companyService: CompanyService,
    private toast: ToastService,
    private router: Router,
    private errorMapper: ErrorMapperService,
    private activeRouter: ActivatedRoute
  ) {
    this.mainForm = this.fb.group({
      name: [
        '',
        [
          Validators.required,
          Validators.minLength(3),
          Validators.maxLength(100),
        ],
      ],
      taxRegistrationNumber: [
        '',
        [
          Validators.required,
          Validators.minLength(5),
          Validators.maxLength(50),
        ],
      ],
    });
  }
  ngOnInit(): void {
    this.activeRouter.paramMap.subscribe((paramMap) => {
      this.id = Number(paramMap.get('Id'));
      if (this.id) {
        this.loadCompany();
      }
    });
  }
  submit() {
    if (this.id) {
      this.put();
    } else {
      this.post();
    }
  }

  post() {
    this.company = this.mainForm.value as ICompany;
    this.companyService.post(this.company).subscribe({
      next: () => this.postSuccess(),
      error: (errVm: IApiErrorVm) => this.fail(errVm),
    });
  }

  put() {
    this.company = this.mainForm.value as ICompany;
    this.companyService.put(this.company, this.id).subscribe({
      next: () => {
        this.putSuccess();
      },
      error: (errVm: IApiErrorVm) => {
        this.fail(errVm);
      },
    });
  }

  private loadCompany() {
    this.companyService.getById(this.id).subscribe({
      next: (data) => {
        this.mainForm.patchValue(data);
      },
      error: (errVm: IApiErrorVm) => {
        this.fail(errVm);
      },
    });
  }
  private putSuccess() {
    this.toast.success('The Data Updated Succsusfully');
    this.router.navigate(['/company/getall']);
  }
  private postSuccess() {
    this.toast.success('The Data Add Succsusfully');
    this.router.navigate(['/company/getall']);
  }
  private fail(errVm: IApiErrorVm) {
    this.errorMapper.getBackEndErrors(this.mainForm, errVm);
    if (this.errorMapper.getToastErrors.length > 0) {
      this.toast.errors(this.errorMapper.getToastErrors);
    }
  }

  get name() {
    return this.mainForm.get('name');
  }
  get taxRegistrationNumber() {
    return this.mainForm.get('taxRegistrationNumber');
  }
}
