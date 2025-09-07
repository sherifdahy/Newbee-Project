import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { WelcomeHome } from './components/welecome-home/welecome-home';

const routes: Routes = [
  {path:'',redirectTo:'Welcome',pathMatch:'full'},
  {path:'Welcome',component:WelcomeHome},
  {path:'**',component:WelcomeHome}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomeRoutingModule { }
