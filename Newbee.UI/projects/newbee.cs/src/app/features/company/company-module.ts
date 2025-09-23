import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CompanyRoutingModule } from './company-routing-module';

import { MainFormCompanyComponent } from './components/main-form-company/main-form-company';
import { GetAllCompanyComponent } from './components/get-all-company/get-all-company';
import { DetailsCompanyComponent } from './components/details-company/details-company';
import { DeleteCompanyComponet } from './components/delete-company/delete-company';
import { SharedModule } from '../../shared/shared-module';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    MainFormCompanyComponent,
    GetAllCompanyComponent,
    DetailsCompanyComponent,
    DeleteCompanyComponet,
  ],
  imports: [
    CommonModule,
    CompanyRoutingModule,
    SharedModule,
    ReactiveFormsModule,
  ],
})
export class CompanyModule {}
