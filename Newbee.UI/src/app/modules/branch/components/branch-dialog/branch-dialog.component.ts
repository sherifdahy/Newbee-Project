import { Component, Inject, OnInit } from '@angular/core';
import { CountryService } from '../../../../core/services/country.service';
import { GovernorateService } from '../../../../core/services/governorate.service';
import { CityService } from '../../../../core/services/city.service';
import { NotificationService } from '../../../../core/services/notification.service';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { BranchResponse } from '../../../../core/models/branch/responses/branch-response';
import { CountryResponse } from '../../../../core/models/country/responses/country-response';
import { GovernorateResponse } from '../../../../core/models/governorate/responses/governorate-response';
import { CityResponse } from '../../../../core/models/city/responses/city-response';
import { BranchRequest } from '../../../../core/models/branch/requests/branch-request';
import { BranchService } from '../../../../core/services/branch.service';

@Component({
  selector: 'app-branch-dialog',
  standalone: false,
  templateUrl: './branch-dialog.component.html',
  styleUrls: ['./branch-dialog.component.css']
})
export class BranchDialogComponent implements OnInit {

  _countries: CountryResponse[] = [];
  _governorates: GovernorateResponse[] = [];
  _cities: CityResponse[] = [];
  _form!: FormGroup;

  constructor(
    private branchService: BranchService,
    private countryService: CountryService,
    private governorateService: GovernorateService,
    private cityService: CityService,
    private notificationService: NotificationService,
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<BranchDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  )
  {
    this._form = this.fb.group({
      code: ['', [Validators.required, Validators.minLength(1), Validators.maxLength(10)]],
      buildingNumber: ['', Validators.required],
      street: ['', Validators.required],
      countryId: ['', Validators.required],
      governorateId: ['', Validators.required],
      cityId: ['', Validators.required],
    });
  }

  ngOnInit() {
    this.loadCountries();

    // Cascading logic
    this._form.get('countryId')?.valueChanges.subscribe((countryId) => {
      if (countryId) {
        this.loadGovernorates(countryId);
        this._form.patchValue({ governorateId: '', cityId: '' });
        this._cities = [];
      }
    });

    this._form.get('governorateId')?.valueChanges.subscribe((governorateId) => {
      if (governorateId) {
        this.loadCities(governorateId);
        this._form.patchValue({ cityId: '' });
      }
    });
  }

  loadCountries() {
    this.countryService.getAll().subscribe({
      next: (res) => this._countries = res,
      error: (err) => this.notificationService.showError(err)
    });
  }

  loadGovernorates(countryId: number) {
    this.governorateService.getRelated(countryId).subscribe({
      next: (res) => this._governorates = res,
      error: (err) => this.notificationService.showError(err)
    });
  }

  loadCities(governorateId: number) {
    this.cityService.getRelated(governorateId).subscribe({
      next: (res) => this._cities = res,
      error: (err) => this.notificationService.showError(err)
    });
  }

  onSubmit() {
    if (this._form.invalid) {
      this._form.markAllAsTouched();
      return;
    }

    let branch = this._form.value as BranchRequest;

    this.branchService.create(this.data.companyId,branch).subscribe({
      next: (res) => {
        this.notificationService.showSuccess('تم حفظ الفرع بنجاح.');
        this.dialogRef.close(true);
      },
      error : (err) => {
        this.notificationService.showError(err);
      }
    });
  }

  onCancel() {
    this.dialogRef.close();
  }
}
