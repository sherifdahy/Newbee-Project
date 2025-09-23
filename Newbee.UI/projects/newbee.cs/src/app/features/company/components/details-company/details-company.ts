import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CompanyService } from '../../../../core/services/backend/company/company.service';
import { ICompany } from '../../../../core/models/company';
import { IApiErrorVm } from '../../../../core/view-models/responses/api-error-response';
import { ErrorMapperService } from '../../../../core/services/frontend/error-mapper/errormapper.service';
import { ToastService } from '../../../../shared/services/toast.service';

@Component({
  selector: 'app-details-company',
  standalone: false,
  templateUrl: './details-company.html',
  styleUrl: './details-company.css',
})
export class DetailsCompanyComponent implements OnInit {
  public company: ICompany = {} as ICompany;
  private id: number = 0;
  constructor(
    private activeRoute: ActivatedRoute,
    private companyService: CompanyService,
    private cdr: ChangeDetectorRef,
    private errorMapper: ErrorMapperService,
    private toast: ToastService
  ) {}
  ngOnInit(): void {
    this.activeRoute.paramMap.subscribe((paramMap) => {
      this.id = Number(paramMap.get('Id'));
      this.getById(this.id);
    });
  }
  getById(id: number) {
    this.companyService.getById(id).subscribe({
      next: (data) => this.getByIdSuccess(data),
      error: (errVm: IApiErrorVm) => this.getByIdFail(errVm),
    });
  }
  private getByIdSuccess(data: ICompany) {
    this.company = data;
    this.cdr.detectChanges();
  }
  private getByIdFail(errVm: IApiErrorVm) {
    this.errorMapper.getBackEndErrors(errVm);
    if (this.errorMapper.getToastErrors.length > 0) {
      this.toast.errors(this.errorMapper.getToastErrors);
    }
  }
}
