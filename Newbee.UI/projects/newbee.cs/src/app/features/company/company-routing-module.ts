import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainFormCompany } from './components/main-form-company/main-form-company';
import { GetAllCompany } from './components/get-all-company/get-all-company';
import { DetailsCompany } from './components/details-company/details-company';
import { DeleteCompany } from './components/delete-company/delete-company';

const routes: Routes = [
  { path: '', redirectTo: 'getall', pathMatch: 'full' },
  { path: 'getall', component: GetAllCompany },
  { path: 'mainform', component: MainFormCompany },
  { path: 'details', component: DetailsCompany },
  { path: 'delete', component: DeleteCompany },
  { path: '**', component: GetAllCompany },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CompanyRoutingModule {}
