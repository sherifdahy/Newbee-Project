import { Component, Inject, Input, OnInit } from '@angular/core';
import { CountryResponse } from '../../../../core/models/country/responses/country-response';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { GovernorateService } from '../../../../core/services/governorate.service';
import { NotificationService } from '../../../../core/services/notification.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CountryService } from '../../../../core/services/country.service';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { GovernorateResponse } from '../../../../core/models/governorate/responses/governorate-response';

@Component({
  selector: 'app-governorate-dialog',
  standalone: false,
  templateUrl: './governorate-dialog.component.html',
  styleUrls: ['./governorate-dialog.component.css']
})
export class GovernorateDialogComponent implements OnInit {

  countries: CountryResponse[] = [];
  form: FormGroup;
  editMode: boolean;
  constructor(
    private fb: FormBuilder,
    private governorateService: GovernorateService,
    private notificationService: NotificationService,
    private dialogRef: MatDialogRef<GovernorateDialogComponent>,
    @Inject(MAT_DIALOG_DATA) private data: any,
    private router: Router,
    private route: ActivatedRoute,
    private countryService: CountryService
  ) {
    this.editMode = data.editMode;
    this.form = this.fb.group({
      name: ['', Validators.required],
      code: ['', Validators.required],
      countryId: [0, [Validators.required, Validators.min(1)]],
    });
  }

  ngOnInit() {
    this.loadCountries();

    if (this.editMode)
      this.loadCurrentGovernorate();
  }

  loadCurrentGovernorate() {
    this.governorateService.get(this.data.id).subscribe({
      next: (response: GovernorateResponse) => {
        this.form.patchValue({
          name: response.name,
          code: response.code,
          countryId: response.country.id
        })
      },
      error: (errors) => {
        this.notificationService.showError(errors);
      }
    })
  }

  loadCountries() {
    this.countryService.getAll().subscribe({
      next: (response) => {
        this.countries = response;
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

    const governorateRequest = this.form.value;

    if (!this.editMode) {
      this.governorateService.create(governorateRequest).subscribe({
        next: () => {
          this.notificationService.showSuccess('تم إنشاء المحافظة بنجاح ✅');
          this.dialogRef.close(true);
        },
        error: (errors) => {
          this.notificationService.showError(errors);
        },
      });
    }
    else {
      this.governorateService.update(this.data.id, governorateRequest).subscribe({
        next: () => {
          this.notificationService.showSuccess('تم تحديث المحافظة بنجاح ✅');
          this.dialogRef.close(true);
        },
        error: (errors) => {
          this.notificationService.showError(errors);
        },
      });
    }

  }

  // ميثود مساعدة لتسهيل الوصول لأي control في HTML
  getControl(controlName: string) {
    return this.form.get(controlName);
  }

}
