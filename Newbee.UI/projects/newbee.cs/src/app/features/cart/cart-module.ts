import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CartRoutingModule } from './cart-routing-module';
import { Test } from './components/test/test';
import {CartService} from './services/cart.service'

@NgModule({
  declarations: [
    Test
  ],
  imports: [
    CommonModule,
    CartRoutingModule
  ],

  providers:[
    CartService
  ]
})
export class CartModule { }
