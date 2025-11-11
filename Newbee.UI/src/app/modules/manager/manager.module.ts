import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ManagerDialogComponent } from './components/manager-dialog/manager-dialog.component';
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { ManagersGridComponent } from './components/managers-grid/managers-grid.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  declarations: [
    ManagerDialogComponent,
    ManagersGridComponent,
  ],
  exports :[
    ManagersGridComponent,
  ]
})
export class ManagerModule { }
