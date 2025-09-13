import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToastrModule } from 'ngx-toastr';
import { ToastService } from './services/toast.service';
import { Spinner } from './style-components/spinner/spinner';
//Import here what ever You Want

@NgModule({
  declarations: [Spinner],
  imports: [CommonModule],
  exports: [ToastrModule, Spinner],
  providers: [ToastService],
})
export class SharedModule {}
