import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { RolesComponent } from './pages/roles/roles.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RoleDialogComponent } from './components/role-dialog/role-dialog.component';

const routes : Routes = [
  {
    path : '',
    component : RolesComponent
  }
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    ReactiveFormsModule,
    FormsModule,
  ],
  declarations: [RolesComponent,RoleDialogComponent]
})
export class RoleModule { }
