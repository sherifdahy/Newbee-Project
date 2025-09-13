import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { SpinnerService } from '../../../core/services/spinner/spinner.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-spinner',
  standalone: false,
  templateUrl: './spinner.html',
  styleUrl: './spinner.css',
})
export class Spinner implements OnInit, OnDestroy {
  showSpinner = false;
  subscriptions: Subscription[] = [];

  constructor(
    private spinnerService: SpinnerService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.subscriptions.push(
      this.spinnerService.loadingAsObservable.subscribe((value) => {
        this.showSpinner = value;
        this.cdr.detectChanges(); // هنا نجبر Angular يـupdate الـ view
      })
    );
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach((s) => s.unsubscribe());
  }
}
