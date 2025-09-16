import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable()
export class SpinnerService {
  private loading: BehaviorSubject<boolean>;
  constructor() {
    this.loading = new BehaviorSubject<boolean>(false);
  }

  show() {
    this.loading.next(true);
  }
  hide() {
    this.loading.next(false);
  }
  get loadingAsObservable() {
    return this.loading.asObservable();
  }
}
