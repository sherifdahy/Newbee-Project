import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ɵInternalFormsSharedModule } from '@angular/forms';
import { CompanyService } from '../../../../core/services/company.service';
import { CompanyRequest } from '../../../../core/models/company/requests/company-request';
import { NotificationService } from '../../../../core/services/notification.service';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-company-dialog',
  standalone : false,
  templateUrl: './company-dialog.component.html',
  styleUrls: ['./company-dialog.component.css'],
})
export class CompanyDialogComponent implements OnInit {

  form! : FormGroup;
  constructor(
    private fb:FormBuilder,
    private companyService : CompanyService,
    private notificationService : NotificationService,
    private dialogRef : MatDialogRef<CompanyDialogComponent>,
  ) { }

  ngOnInit() {
    this.form = this.fb.group({
      name : ['',[Validators.required]],
      rin : ['',[Validators.required]]
    })
  }

  onSubmit(){
    if(this.form.invalid)
    {
      this.form.markAllAsTouched();
      return;
    }

    let company = this.form.value as CompanyRequest;

    this.companyService.create(company).subscribe({
      next :(response)=>{
        this.dialogRef.close(response);
      },
      error :(errors)=>{
        this.notificationService.showSuccess(errors);
      }
    });
  }

}
