import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HomeRoutingModule } from './home-routing-module';
import { WelcomeHome } from './components/welecome-home/welecome-home';


@NgModule({
  declarations: [
    WelcomeHome
  ],
  imports: [
    CommonModule,
    HomeRoutingModule
  ]
})
export class HomeModule { }
