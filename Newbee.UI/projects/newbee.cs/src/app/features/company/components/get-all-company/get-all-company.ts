import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { CompanyService } from '../../../../core/services/backend/company/company.service';
import { ICompany } from '../../../../core/models/company';
import { IApiErrorVm } from '../../../../core/view-models/responses/api-error-response';
import { ToastService } from '../../../../shared/services/toast.service';
import { ErrorMapperService } from '../../../../core/services/frontend/error-mapper/errormapper.service';

@Component({
  selector: 'app-get-all-company',
  standalone: false,
  templateUrl: './get-all-company.html',
  styleUrls: ['./get-all-company.css'],
})
export class GetAllCompanyComponent implements OnInit {
  public companies: ICompany[] = [];
  constructor(
    private companyService: CompanyService,
    private toast: ToastService,
    private cdr: ChangeDetectorRef,
    private errorMapper: ErrorMapperService
  ) {}

  ngOnInit() {
    this.companyService.getAll().subscribe({
      next: (data) => this.getAllSuccess(data),
      error: (err: IApiErrorVm) => this.getAllFail(err),
    });
  }
  private getAllSuccess(data: ICompany[]) {
    this.companies = data;
    this.cdr.detectChanges();
  }

  private getAllFail(errVm: IApiErrorVm) {
    this.errorMapper.getBackEndErrors(errVm);
    if (this.errorMapper.getToastErrors.length > 0) {
      this.toast.errors(this.errorMapper.getToastErrors);
    }
  }
}
