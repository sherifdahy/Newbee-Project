import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NotificationService } from '../../../../core/services/notification.service';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { PointOfSaleService } from '../../../../core/services/point-of-sale.service';
import { PointOfSaleRequest } from '../../../../core/models/point-of-sale/requests/point-of-sale-request';

@Component({
  selector: 'app-point-of-sale-dialog',
  standalone : false,
  templateUrl: './point-of-sale-dialog.component.html',
  styleUrls: ['./point-of-sale-dialog.component.css']
})
export class PointOfSaleDialogComponent implements OnInit {

  posForm!: FormGroup;

  constructor(
    private pointOfSaleService: PointOfSaleService,
    private notificationService: NotificationService,
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<PointOfSaleDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
  ) {
    this.posForm = this.fb.group({
      posSerial: ['', Validators.required],
      clientId: ['', Validators.required],
      clientSecret: ['', Validators.required],
    });
  }

  ngOnInit() {
    if(this.data?.posId)
    {
      this.loadCurrentPointOfSale();
    }
  }

  onSubmit()
  {
    if (this.posForm.invalid) {
      this.posForm.markAllAsTouched();
      return;
    }

    const posData = this.posForm.value as PointOfSaleRequest;

    let obj = {
      next : (response : any)=>{
        this.dialogRef.close(true);
      },
      error :(errors:any)=>{
        this.notificationService.showError(errors);
      }
    }

    if(this.data?.posId)
    {
      this.pointOfSaleService.update(this.data.posId, posData).subscribe(obj);
    }
    else
    {
      this.pointOfSaleService.create(this.data.branchId,posData).subscribe(obj);
    }
  }
  onCancel(){
    this.dialogRef.close(false);
  }
  loadCurrentPointOfSale()
  {
    this.pointOfSaleService.get(this.data.posId).subscribe({
      next: (response) => {
        this.posForm.patchValue({
          posSerial: response.posSerial,
        });
      },
      error: (errors) => {
        this.notificationService.showError(errors);
      }
    });
  }

}
