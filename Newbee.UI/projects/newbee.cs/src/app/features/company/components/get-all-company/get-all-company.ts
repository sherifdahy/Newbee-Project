import { Component, OnInit } from '@angular/core';
import { CompanyService } from '../../../../core/services/backend/company/company.service';
import { ICompany } from '../../../../core/models/company';
import { ToastrService } from 'ngx-toastr';
import { IApiErrorVm } from '../../../../core/view-models/responses/api-error-response';

@Component({
  selector: 'app-get-all-company',
  standalone: false,
  templateUrl: './get-all-company.html',
  styleUrl: './get-all-company.css',
})
export class GetAllCompany implements OnInit {
  public companies: ICompany[] = [];
  constructor(
    private companyService: CompanyService,
    private toast: ToastrService
  ) {}

  ngOnInit() {
    this.companyService.getAll().subscribe(
      (data) => {
        this.companies = data;
      }
      // (err: IApiErrorVm) => {
      //   err.errors.array.forEach((element) => {
      //     this.toast.error(element);
      //   });
      // }
    );
  }
}
