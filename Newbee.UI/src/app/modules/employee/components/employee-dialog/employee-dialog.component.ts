import { Component, Inject, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { NotificationService } from '../../../../core/services/notification.service';
import { EmployeeService } from '../../../../core/services/employee.service';
import { Dialog, DialogRef } from '@angular/cdk/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-employee-dialog',
  standalone : false,
  templateUrl: './employee-dialog.component.html',
  styleUrls: ['./employee-dialog.component.css']
})
export class EmployeeDialogComponent implements OnInit {

  employeeFrom!: FormGroup;

  constructor(
    private fb : FormBuilder,
    private notificationService: NotificationService,
    private employeeService: EmployeeService,
    private dialogRef: MatDialogRef<EmployeeDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { branchId: number, employeeId: number }
  ) { }

  ngOnInit() {
    this.employeeFrom = this.fb.group({
      name: ['', Validators.required],
      identifierNumber : ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password : ['', Validators.required],
      phoneNumber: ['', Validators.required],
    });

    this.data.employeeId ? this.loadCurrentEmployee(this.data.employeeId) : null;
  }

  onSubmit() {
    alert(this.data.branchId);
    if (this.employeeFrom.valid) {
      if (this.data.employeeId) {
        this.employeeService.update(this.data.branchId,this.employeeFrom).subscribe({
          next: () => {
            this.notificationService.showSuccess("تم تحديث بيانات الموظف بنجاح.");
            this.dialogRef.close(true);
          },
          error : (errors)=>{
            this.notificationService.showError(errors);
          }
        });
      }
      else {
        this.employeeService.create(this.data.branchId, this.employeeFrom.value).subscribe({
          next: () => {
            this.notificationService.showSuccess("تم إضافة الموظف بنجاح.");
            this.dialogRef.close(true);
          },
          error: (errors) => {
            this.notificationService.showError(errors);
          }
        });
      }
    }
  }

  loadCurrentEmployee(employeeId: number) {
    this.employeeService.get(employeeId).subscribe({
      next: (response) => {
        this.employeeFrom.patchValue(response);
      }
    });
  }

}

