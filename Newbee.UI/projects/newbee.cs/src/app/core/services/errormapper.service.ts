import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Injectable({ providedIn: 'root' })
export class ErrorMapperService {
  mapBackendErrors(
    form: FormGroup,
    errors: Record<string, string[]>
  ): string[] {
    let globalErrors: string[] = [];
    for (const field in errors) {
      if (!errors.hasOwnProperty(field)) continue;

      const control = form.get(field.charAt(0).toLowerCase() + field.slice(1));
      if (control) {
        alert('Control');
        const backendMessage = errors[field][0]; // ناخد أول رسالة بس
        control.setErrors({ backend: backendMessage });
        control.markAsTouched();
      } else {
        alert('Not Control');
        globalErrors.push(...errors[field].map((msg) => `${field}: ${msg}`));
      }
    }
    return globalErrors;
  }
}
