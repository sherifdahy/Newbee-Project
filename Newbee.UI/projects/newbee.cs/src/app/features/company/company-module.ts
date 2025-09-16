import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CompanyRoutingModule } from './company-routing-module';

import { MainFormCompany } from './components/main-form-company/main-form-company';
import { GetAllCompany } from './components/get-all-company/get-all-company';
import { DetailsCompany } from './components/details-company/details-company';
import { DeleteCompany } from './components/delete-company/delete-company';

@NgModule({
  declarations: [MainFormCompany, GetAllCompany, DetailsCompany, DeleteCompany],
  imports: [CommonModule, CompanyRoutingModule],
})
export class CompanyModule {}
