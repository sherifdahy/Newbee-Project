import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Test } from './components/test/test';

const routes: Routes = [

  {path:'',redirectTo:'Test',pathMatch:'full'},
  {path:'Test',component:Test}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CartRoutingModule { }
