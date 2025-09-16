import { HttpErrorResponse } from '@angular/common/http';
import { IApiErrorVm } from '../../view-models/responses/api-error-response';
import { throwError } from 'rxjs';

export function handleError(error: HttpErrorResponse) {
  if (error.error && error.error.errors) {
    return throwError(() => error.error as IApiErrorVm);
  }

  return throwError(
    () =>
      ({
        title: 'An unexpected error occurred, please try again',
        status: error.status,
        errors: {},
      } as IApiErrorVm)
  );
}
