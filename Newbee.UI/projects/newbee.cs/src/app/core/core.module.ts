import { NgModule, Optional, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';

//here import services & incepteros & guards
import { ApiService } from './services/api/api.service';
import { AuthService } from './services/auth/auth.service';
import { LocalStorgeService } from './services/local-storge/local-storge.service';
import { MainLayout } from './layout/main-layout/main-layout';
import { Header } from './layout/main-layout/header/header';
import { Footer } from './layout/main-layout/footer/footer';
import { AppRoutingModule } from '../app-routing-module';
import { SecondaryLayout } from './layout/secondary-layout/secondary-layout';
import { SideBar } from './layout/main-layout/side-bar/side-bar';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AuthInterceptor } from './interceptors/auth-interceptor';

@NgModule({
  declarations: [MainLayout, Header, Footer, SecondaryLayout, SideBar],
  imports: [CommonModule, AppRoutingModule, HttpClientModule],
  providers: [
    ApiService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    },
    AuthService,
    LocalStorgeService,
  ],
})
export class CoreModule {}
