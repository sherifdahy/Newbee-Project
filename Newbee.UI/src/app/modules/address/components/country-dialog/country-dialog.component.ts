import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NotificationService } from '../../../../core/services/notification.service';
import { CountryService } from '../../../../core/services/country.service';
import { CountryRequest } from '../../../../core/models/country/requests/country-request';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CountryResponse } from '../../../../core/models/country/responses/country-response';

@Component({
  selector: 'app-country-dialog',
  standalone: false,
  templateUrl: './country-dialog.component.html',
  styleUrls: ['./country-dialog.component.css']
})
export class CountryDialogComponent implements OnInit {

  editMode: boolean;
  form!: FormGroup;
  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private notificationService: NotificationService,
    private fb: FormBuilder,
    @Inject(MAT_DIALOG_DATA) private data: any,
    private dialogRef : MatDialogRef<CountryDialogComponent>,
    private countryService: CountryService
  ) {
    this.editMode = data.editMode;
    this.form = fb.group({
      name: fb.control('', [Validators.required]),
      code: fb.control('', [Validators.required])
    });
  }

  ngOnInit() {
    if (this.editMode)
      this.loadCurrentCountry();
  }
  getControl(controlName: string) {
    return this.form.get(controlName);
  }

  loadCurrentCountry() {
    this.countryService.get(this.data.id).subscribe({
      next: (response: CountryResponse) => {
        this.form.patchValue({
          name: response.name,
          code: response.code
        })
      },
      error: (errors) => {
        this.notificationService.showError(errors);
      }
    })
  }

  handleSubmitForm() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    let countryRequest = this.form.value as CountryRequest;

    if (this.editMode) {
      this.countryService.update(this.data.id, countryRequest).subscribe({
        next: () => {
          this.notificationService.showSuccess('done');
          this.dialogRef.close(true);
        },
        error: (errors) => {
          this.notificationService.showError(errors);
        }
      });
    }
    else {
      this.countryService.create(countryRequest).subscribe({
        next: () => {
          this.notificationService.showSuccess('done');
          this.dialogRef.close(true);
        },
        error: (errors) => {
          this.notificationService.showError(errors);
        }
      });
    }
  }

}
