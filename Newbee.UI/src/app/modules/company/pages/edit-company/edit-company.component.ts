import { Component, ElementRef, OnInit, output, Output, ViewChild } from '@angular/core';
import { Location } from '@angular/common';
import { CompanyRequest } from '../../../../core/models/company/requests/company-request';
import {  FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ActiveCodeService } from '../../../../core/services/active-code.service';
import { NotificationService } from '../../../../core/services/notification.service';
import { CompanyService } from '../../../../core/services/company.service';
import { ActiveCodeResponse } from '../../../../core/models/active-code/response/active-code-response';
import { BranchDialogComponent } from '../../../branch/components/branch-dialog/branch-dialog.component';
import { ManagersGridComponent } from '../../../manager/components/managers-grid/managers-grid.component';
import { BranchService } from '../../../../core/services/branch.service';
import { BranchResponse } from '../../../../core/models/branch/responses/branch-response';
import { MatDialog } from '@angular/material/dialog';
import { PointOfSaleDialogComponent } from '../../../point-of-sale/components/point-of-sale-dialog/point-of-sale-dialog.component';
import { PointOfSaleGridComponent } from '../../../point-of-sale/components/point-of-sale-grid/point-of-sale-grid.component';
import { ManagerDialogComponent } from '../../../manager/components/manager-dialog/manager-dialog.component';
import { EmployeeDialogComponent } from '../../../employee/components/employee-dialog/employee-dialog.component';
import { EmployeeGridComponent } from '../../../employee/components/employee-grid/employee-grid.component';

@Component({
  selector: 'app-edit-company',
  standalone: false,
  templateUrl: './edit-company.component.html',
  styleUrls: ['./edit-company.component.css']
})
export class EditCompanyComponent implements OnInit {
  _companyId!: number;
  _companyForm!: FormGroup;
  _activeCodes!: ActiveCodeResponse[];
  _branches!: BranchResponse[];

  selectedBranchId!: number;

  @ViewChild(PointOfSaleGridComponent) pointOfSaleGridComponent!: PointOfSaleGridComponent;
  @ViewChild(ManagersGridComponent) managersGridComponent!: ManagersGridComponent;
  @ViewChild(EmployeeGridComponent) employeeGridComponent!: EmployeeGridComponent;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private activeCodeService: ActiveCodeService,
    private notificationService: NotificationService,
    private companyService: CompanyService,
    private branchService: BranchService,
    private location: Location,
    private dialog: MatDialog,
  ) {
    this._companyId = Number(this.route.snapshot.paramMap.get('id'));
  }

  ngOnInit(): void {

    this._companyForm = this.fb.group({
      name: ['', Validators.required],
      rin: ['', Validators.required],
    });

    this.loadActiveCodes();
    this.loadBranches();
    this.loadCurrentCompany();
  }

  goBack(): void {
    this.location.back();
  }


  updateCompany() {
    if (this._companyForm.valid && this._companyForm.dirty) {
      let company = this._companyForm.value as CompanyRequest;

      this.companyService.update(this._companyId, company).subscribe({
        next: () => {
          this.notificationService.showSuccess('done');
          this._companyForm.markAsPristine();
        },
        error: (errors) => {
          this.notificationService.showError(errors);
        }
      })
    }

  }

  loadCurrentCompany() {
    this.companyService.get(this._companyId).subscribe({
      next: (response) => {
        this._companyForm.patchValue({
          name: response.name,
          rin: response.rin,
        });
      },
      error: (errors) => {
        this.notificationService.showError(errors);
      }
    })
  }



  addCode(): void {
    // this.activeCodes.push(this.fb.control('', Validators.required));
  }


  loadActiveCodes() {
    this.activeCodeService.getAll().subscribe({
      next: (response) => {
        this._activeCodes = response;
      },
      error: (errors) => {
        this.notificationService.showError(errors);
      }
    });
  }

  loadBranches() {
    this.branchService.getAll(this._companyId).subscribe({
      next: (response) => {
        this._branches = response;
      },
      error: (errors) => {
        this.notificationService.showError(errors);
      }
    });
  }


  openBranchDialog(branchId: number) {
    let dialogRef = this.dialog.open(BranchDialogComponent, {
      data: {
        companyId: this._companyId,
        branchId: branchId,
      },
      minWidth: '60vw',
    });

    dialogRef.afterClosed().subscribe((result: boolean) => {
      if (result) {
        this.loadBranches();
      }
    });
  }
  openManagerDialog(managerId: number) {
    let dialogRef = this.dialog.open(ManagerDialogComponent, {
      data: {
        companyId: this._companyId,
        managerId: managerId
      },
      minWidth: '60vw'
    });

    dialogRef.afterClosed().subscribe((result: boolean) => {
      if (result) {
        this.managersGridComponent.loadManagers(this._companyId);
      }
    });
  }

  openPointOfSaleDialog(posId: number) {
    let dialogRef = this.dialog.open(PointOfSaleDialogComponent, {
      data: {
        posId: posId,
        branchId: this.selectedBranchId,
      },
      minWidth: '40vw',
    });

    dialogRef.afterClosed().subscribe((result: boolean) => {
      if (result) {
        this.pointOfSaleGridComponent.loadPointOfSales();
        this.notificationService.showSuccess("تم حفظ نقطة البيع بنجاح.");
      }
    });
  }

  openEmployeeDialog(employeeId: number) {
    let dialogRef = this.dialog.open(EmployeeDialogComponent, {
      data: {
        employeeId: employeeId,
        branchId: this.selectedBranchId,
      },
      minWidth: '60vw',
    });

    dialogRef.afterClosed().subscribe((result: boolean) => {
      if (result) {
        this.employeeGridComponent.loadEmployees();
        this.notificationService.showSuccess("تم حفظ الموظف بنجاح.");
      }
    });
  }

  removeCode(index: number): void {
    // this.activeCodes.removeAt(index);
  }

}
