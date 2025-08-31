import { NgModule, Optional, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';

//here import services & incepteros & guards 
import { ApiService } from './services/api.service'
import { AuthService } from './services/auth.service'


@NgModule({
  declarations: [],
  imports: [
    CommonModule
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
