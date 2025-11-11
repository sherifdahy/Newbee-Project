import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeeGridComponent } from './components/employee-grid/employee-grid.component';
import { EmployeeDialogComponent } from './components/employee-dialog/employee-dialog.component';
import { FormsModule, NgForm, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  imports: [
    ReactiveFormsModule,
    FormsModule,
    CommonModule
  ],
  declarations: [
    EmployeeGridComponent,
    EmployeeDialogComponent,
  ],
  exports : [
    EmployeeGridComponent
  ],
})
export class EmployeeModule { }
