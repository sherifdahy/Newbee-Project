import { NgModule, provideBrowserGlobalErrorListeners, provideZonelessChangeDetection } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing-module';
import { App } from './app';
import { CoreModule } from './core/core.module';
import { NotFoundPage } from './pages/not-found-page/not-found-page';
import { UnauthorizedPage } from './pages/unauthorized-page/unauthorized-page';

@NgModule({
  declarations: [
    App,
    NotFoundPage,
    UnauthorizedPage
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    CoreModule
  ],
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideZonelessChangeDetection()
  ],
  bootstrap: [App]
})
export class AppModule { }
