import { Component } from '@angular/core';

@Component({
  selector: 'app-checkout',
  standalone: false,
  templateUrl: './checkout.component.html',
  styleUrl: './checkout.component.css'
})
export class CheckoutComponent {
  orderTotal = 18900;
  currentStep = 1;

  goToStep(step: number) {
    this.currentStep = step;
  }
}
