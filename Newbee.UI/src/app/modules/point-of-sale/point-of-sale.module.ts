import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PointOfSaleDialogComponent } from './components/point-of-sale-dialog/point-of-sale-dialog.component';
import { FormsModule, NgForm, ReactiveFormsModule } from '@angular/forms';
import { PointOfSaleGridComponent } from './components/point-of-sale-grid/point-of-sale-grid.component';

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
  ],
  declarations: [
    PointOfSaleDialogComponent,
    PointOfSaleGridComponent,
  ],
  exports :[
    PointOfSaleDialogComponent,
    PointOfSaleGridComponent,
  ]
})
export class PointOfSaleModule { }
