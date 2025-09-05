import { NgModule, Optional, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';

//here import services & incepteros & guards
import { ApiService } from './services/api.service'
import { AuthService } from './services/auth.service';
import { MainLayout } from './layout/main-layout/main-layout';
import { Header } from './layout/main-layout/header/header';
import { Footer } from './layout/main-layout/footer/footer'
import { AppRoutingModule } from "../app-routing-module";
import { SecondaryLayout } from './layout/secondary-layout/secondary-layout';
import { SideBar } from './layout/main-layout/side-bar/side-bar';
import { HttpClientModule } from '@angular/common/http';


@NgModule({
  declarations: [
    MainLayout,
    Header,
    Footer,
    SecondaryLayout,
    SideBar
  ],
  imports: [
    CommonModule,
    AppRoutingModule,
    HttpClientModule
],
  providers: [
    ApiService,
    AuthService
  ]
})
export class CoreModule {

  //To Import the
  // constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
  //   if (parentModule) {
  //     throw new Error('CoreModule is already loaded. Import it in the AppModule only.');
  //   }
  // }
}
