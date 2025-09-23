import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CompanyService } from '../../../../core/services/backend/company/company.service';
import { IApiErrorVm } from '../../../../core/view-models/responses/api-error-response';
import { ErrorMapperService } from '../../../../core/services/frontend/error-mapper/errormapper.service';
import { ToastService } from '../../../../shared/services/toast.service';

@Component({
  selector: 'app-delete-company',
  standalone: false,
  templateUrl: './delete-company.html',
  styleUrl: './delete-company.css',
})
export class DeleteCompanyComponet implements OnInit {
  private id: number = 0;
  constructor(
    private activeRoute: ActivatedRoute,
    private companyService: CompanyService,
    private toast: ToastService,
    private router: Router,
    private errorMapper: ErrorMapperService
  ) {}
  ngOnInit(): void {
    this.activeRoute.paramMap.subscribe((paramMap) => {
      this.id = Number(paramMap.get('Id'));
      this.delete(this.id);
    });
  }
  delete(id: number) {
    this.companyService.delete(id).subscribe({
      next: () => {
        this.deleteSuccess();
      },
      error: (errVm: IApiErrorVm) => {
        this.deleteFail(errVm);
      },
    });
  }

  private deleteSuccess() {
    this.toast.success('Delete Succsess');
    this.router.navigate(['/company/getall']);
  }
  private deleteFail(errVm: IApiErrorVm) {
    this.errorMapper.getBackEndErrors(errVm);
    if (this.errorMapper.getToastErrors.length > 0) {
      this.toast.errors(this.errorMapper.getToastErrors);
    }
  }
}
