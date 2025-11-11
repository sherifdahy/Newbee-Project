import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PortalDashboardComponent } from './pages/portal-dashboard.component';
import { RouterModule, Routes } from '@angular/router';

const routes : Routes = [
  {
    path : '',
    component : PortalDashboardComponent,
  }
]

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
  ],
  declarations: [PortalDashboardComponent]
})
export class PortalDashboardModule { }
