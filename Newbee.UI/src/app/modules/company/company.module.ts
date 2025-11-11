import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CompaniesComponent } from './pages/companies/companies.component';
import { BranchDialogComponent } from '../branch/components/branch-dialog/branch-dialog.component';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { MatTableModule } from '@angular/material/table';
import { MatTabsModule } from '@angular/material/tabs';
import { CompanyDialogComponent } from './components/company-dialog/company-dialog.component';
import { EditCompanyComponent } from './pages/edit-company/edit-company.component';
import { ManagerModule } from '../manager/manager.module';
import { EmployeeModule } from "../employee/employee.module";
import { PointOfSaleGridComponent } from "../point-of-sale/components/point-of-sale-grid/point-of-sale-grid.component";
import { PointOfSaleModule } from '../point-of-sale/point-of-sale.module';

const routes: Routes = [
  {
    path: 'companies',
    children: [
      {
        path: '',
        component: CompaniesComponent
      },
      {
        path: ':id',
        component: EditCompanyComponent
      }
    ]
  },

];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    ReactiveFormsModule,
    FormsModule,
    // Material modules
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatIconModule,
    MatDividerModule,
    MatTableModule,
    MatTabsModule,
    // custom modules
    ManagerModule,
    EmployeeModule,
    PointOfSaleModule,
],
  declarations: [
    CompaniesComponent,
    EditCompanyComponent,
    BranchDialogComponent,
    CompanyDialogComponent
  ]
})
export class CompanyModule { }
