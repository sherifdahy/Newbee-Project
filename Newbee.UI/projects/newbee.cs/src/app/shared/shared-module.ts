import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToastrModule } from 'ngx-toastr';
import {ToastService} from './services/toast.service'
//Import here what ever You Want




@NgModule({
  declarations: [
  ],
  imports: [
    CommonModule
  ],
  exports: [ToastrModule]  ,
  providers:[ToastService]
})
export class SharedModule { }
