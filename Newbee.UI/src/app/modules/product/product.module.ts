import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ProductsComponent } from './pages/products/products.component';

const routes : Routes =[
  {
    path :'',
    component : ProductsComponent,
  }
];

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    RouterModule.forChild(routes),
  ],
  declarations: [
    ProductsComponent,
  ]
})
export class ProductModule { }
