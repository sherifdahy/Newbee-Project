import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../../../environments/environment.prod';
import { catchError, Observable } from 'rxjs';
import { handleError } from '../generic-handel-error';

@Injectable({ providedIn: 'root' })
export class GenericService<T> {
  private apiUrl: string = environment.apiBaseUrl;
  constructor(private http: HttpClient) {}
  getAll(endPoint: string): Observable<T[]> {
    return this.http
      .get<T[]>(`${this.apiUrl}/${endPoint}`)
      .pipe(catchError(handleError));
  }
  getById(endPoint: string, id: number): Observable<T> {
    return this.http
      .get<T>(`${this.apiUrl}/${endPoint}/${id}`)
      .pipe(catchError(handleError));
  }
  post(endPoint: string, data: T): Observable<T> {
    return this.http
      .post<T>(`${this.apiUrl}/${endPoint}`, data)
      .pipe(catchError(handleError));
  }
  put(endPoint: string, data: T, id: number): Observable<T> {
    return this.http
      .put<T>(`${this.apiUrl}/${endPoint}/${id}`, data)
      .pipe(catchError(handleError));
  }
  delete(endPoint: string, id: number): Observable<void> {
    return this.http
      .delete<void>(`${this.apiUrl}/${endPoint}/${id}`)
      .pipe(catchError(handleError));
  }
}
