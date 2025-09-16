import { NgModule, Optional, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';

//here import services & incepteros & guards
import { GenericService } from './services/backend/generic/generic.service';
import { AuthService } from './services/backend/auth/auth.service';
import { LocalStorgeService } from './services/frontend/local-storge/local-storge.service';
import { MainLayout } from './layout/main-layout/main-layout';
import { Header } from './layout/main-layout/header/header';
import { Footer } from './layout/main-layout/footer/footer';
import { AppRoutingModule } from '../app-routing-module';
import { SecondaryLayout } from './layout/secondary-layout/secondary-layout';
import { SideBar } from './layout/main-layout/side-bar/side-bar';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AuthInterceptor } from './interceptors/auth/auth-interceptor';
import { SpinnerService } from './services/frontend/spinner/spinner.service';
import { SpinnerInterceptor } from './interceptors/spinner/spinner-interceptor';
import { CompanyService } from './services/backend/company/company.service';
@NgModule({
  declarations: [MainLayout, Header, Footer, SecondaryLayout, SideBar],
  imports: [CommonModule, AppRoutingModule, HttpClientModule],
  providers: [
    GenericService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    },
    { provide: HTTP_INTERCEPTORS, useClass: SpinnerInterceptor, multi: true },

    AuthService,
    LocalStorgeService,
    SpinnerService,
    CompanyService,
  ],
})
export class CoreModule {}
