import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { IApiErrorVm } from '../../../view-models/responses/api-error-response';

@Injectable({ providedIn: 'root' })
export class ErrorMapperService {
  private toastErrors: string[] = [];

  get getToastErrors() {
    return this.toastErrors;
  }

  getBackEndErrors(form: FormGroup, errVm: IApiErrorVm): void;
  getBackEndErrors(errVm: IApiErrorVm): void;
  getBackEndErrors(
    formOrErr: FormGroup | IApiErrorVm,
    errVm?: IApiErrorVm
  ): void {
    this.toastErrors = []; // reset each time

    if (formOrErr instanceof FormGroup && errVm) {
      this.mapBackendErrors(formOrErr, errVm.errors);
    } else {
      const onlyErrVm = formOrErr as IApiErrorVm;
      this.mapToastBackEndErrors(onlyErrVm.errors);
    }
  }

  private mapBackendErrors(form: FormGroup, errors: Record<string, string[]>) {
    for (const field in errors) {
      if (!errors.hasOwnProperty(field)) continue;
      const control = form.get(this.toControlName(field));
      if (control) {
        this.assignTheErrorToRelatedControl(control, errors, field);
      } else {
        errors[field].forEach((msg) => this.addToToastrErrors(msg));
      }
    }
  }

  private mapToastBackEndErrors(errors: Record<string, string[]>) {
    for (const field in errors) {
      if (!errors.hasOwnProperty(field)) continue;
      else {
        errors[field].forEach((msg) => this.addToToastrErrors(msg));
      }
    }
  }
  //API usually returns PascalCase; convert to camelCase
  private toControlName(field: string): string {
    return field.charAt(0).toLowerCase() + field.slice(1);
  }

  private assignTheErrorToRelatedControl(
    control: any,
    errors: Record<string, string[]>,
    field: string
  ) {
    const backendMessage = errors[field][0];
    control.setErrors({ backend: backendMessage });
    control.markAsTouched();
  }

  private addToToastrErrors(error: string) {
    this.toastErrors.push(error);
  }
}
