import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

//LazyLoading The Cart Module
const routes: Routes = [
  {path:'cart',loadChildren:()=>import('./features/cart/cart-module').then(m=>m.CartModule)}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
