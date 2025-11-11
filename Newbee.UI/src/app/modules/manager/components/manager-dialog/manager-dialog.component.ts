import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ManagerService } from '../../../../core/services/manager.service';
import { ManagerRequest } from '../../../../core/models/manager/requests/manager-request';
import { NotificationService } from '../../../../core/services/notification.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-manager-dialog',
  standalone: false,
  templateUrl: './manager-dialog.component.html',
  styleUrls: ['./manager-dialog.component.css']
})
export class ManagerDialogComponent implements OnInit {
  form!: FormGroup;
  constructor(
    private fb: FormBuilder,
    private notificationService : NotificationService,
    private dialogRef: MatDialogRef<ManagerDialogComponent>,
    private managerService : ManagerService,
    @Inject(MAT_DIALOG_DATA) public data: any,
  ) {}

  ngOnInit(): void {
    this.formInit();
    this.loadCurrentManager();
  }

  loadCurrentManager(){
    if(this.data.managerId)
    {
      this.managerService.get(this.data.managerId).subscribe({
        next:(respone)=>{
          this.form.patchValue(respone);
        },
        error :(errors)=>{
          this.notificationService.showError(errors);
        }
      });
    }
  }

  formInit() {
    this.form = this.fb.group({
      id : [0,Validators.required],
      name: ['', [Validators.required, Validators.minLength(3)]],
      identifierNumber: ['', [Validators.required, Validators.pattern(/^[0-9]+$/)]],
      phoneNumber: ['', [Validators.required, Validators.pattern(/^01[0-9]{9}$/)]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8)]],
    });
  }

  get f() {
    return this.form.controls;
  }

  onSubmit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    let manager = this.form.value as ManagerRequest;

    let obj = {
      next :()=>{
        this.dialogRef.close(true);
      },
      error:(errors :any)=>{
        this.notificationService.showError(errors);
      }
    };
    this.data.managerId?
      this.managerService.update(this.data.companyId,this.data.managerId,manager).subscribe(obj) :
      this.managerService.create(this.data.companyId,manager).subscribe(obj) ;
  }

  onClose() {
    this.dialogRef.close(false);
  }
}
