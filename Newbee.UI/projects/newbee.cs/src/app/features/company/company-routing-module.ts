import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainFormCompanyComponent } from './components/main-form-company/main-form-company';
import { GetAllCompanyComponent } from './components/get-all-company/get-all-company';
import { DetailsCompanyComponent } from './components/details-company/details-company';
import { DeleteCompanyComponet } from './components/delete-company/delete-company';

const routes: Routes = [
  { path: '', redirectTo: 'getall', pathMatch: 'full' },
  { path: 'getall', component: GetAllCompanyComponent },
  { path: 'mainform', component: MainFormCompanyComponent },
  { path: 'mainform/:Id', component: MainFormCompanyComponent },
  { path: 'details/:Id', component: DetailsCompanyComponent },
  { path: 'delete/:Id', component: DeleteCompanyComponet },
  { path: '**', component: GetAllCompanyComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CompanyRoutingModule {}
