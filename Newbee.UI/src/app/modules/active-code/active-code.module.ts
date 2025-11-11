import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActiveCodesComponent } from './pages/active-codes/active-codes.component';
import { RouterModule, Routes } from '@angular/router';
import { ActiveCodeDialogComponent } from './components/active-code-dialog/active-code-dialog.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

const routes : Routes = [
  {
    path : 'active-codes',
    component : ActiveCodesComponent
  }
]

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    ReactiveFormsModule,
    FormsModule,
  ],
  declarations: [ActiveCodesComponent,ActiveCodeDialogComponent]
})
export class ActiveCodeModule { }
