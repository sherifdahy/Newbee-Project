import { HttpErrorResponse } from '@angular/common/http';
import { IApiErrorVm } from '../../view-models/responses/api-error-response';
import { throwError } from 'rxjs';

export function handleError(httpError: HttpErrorResponse) {
  if (httpError.error && httpError.error.errors) {
    return throwError(() => httpError.error as IApiErrorVm);
  }

  return throwError(
    () =>
      ({
        title: 'An unexpected error occurred, please try again',
        status: httpError.status,
        errors: {},
      } as IApiErrorVm)
  );
}
