import { Component, Inject, Input, OnInit } from '@angular/core';
import { GovernorateResponse } from '../../../../core/models/governorate/responses/governorate-response';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { GovernorateService } from '../../../../core/services/governorate.service';
import { NotificationService } from '../../../../core/services/notification.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CityService } from '../../../../core/services/city.service';
import { CityRequest } from '../../../../core/models/city/requests/city-request';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-city-dialog',
  standalone: false,
  templateUrl: './city-dialog.component.html',
  styleUrls: ['./city-dialog.component.css']
})
export class CityDialogComponent implements OnInit {

  governorates: GovernorateResponse[] = [];
  form: FormGroup;
  editMode: boolean;
  constructor(
    private fb: FormBuilder,
    private governorateService: GovernorateService,
    private notificationService: NotificationService,
    private router: Router,
    private route: ActivatedRoute,
    private cityService: CityService,
    private dialogRef: MatDialogRef<CityDialogComponent>,
    @Inject(MAT_DIALOG_DATA) private data: any,
  ) {
    this.editMode = data.editMode;
    this.form = this.fb.group({
      name: ['', Validators.required],
      code: ['', Validators.required],
      governorateId: [0, [Validators.required, Validators.min(1)]],
    });
  }

  ngOnInit() {
    this.loadGovernorates();

    if (this.editMode)
      this.loadCurrentCity();
  }

  loadCurrentCity() {
    this.cityService.get(this.data.id).subscribe({
      next: (response) => {
        this.form.patchValue({
          name: response.name,
          code: response.code,
          governorateId: response.governorate.id
        })
      },
      error: (errors) => {
        this.notificationService.showError(errors);
      }
    })
  }

  loadGovernorates() {
    this.governorateService.getAll().subscribe({
      next: (response) => {
        this.governorates = response;
      },
      error: (errors) => {
        this.notificationService.showError(errors);
      },
    });
  }

  handleSubmitForm() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const request = this.form.value as CityRequest;

    if (this.editMode) {
      this.cityService.update(this.data.id, request).subscribe({
        next: () => {
          this.notificationService.showSuccess('تم التحديث المدينة بنجاح ✅');
          this.dialogRef.close(true);
        },
        error: (errors) => {
          this.notificationService.showError(errors);
        },
      });
    }
    else {
      this.cityService.create(request).subscribe({
        next: () => {
          this.notificationService.showSuccess('تم إنشاء المدينة بنجاح ✅');
          this.dialogRef.close(true);
        },
        error: (errors) => {
          this.notificationService.showError(errors);
        },
      });
    }
  }

  getControl(controlName: string) {
    return this.form.get(controlName);
  }

}
