import { Component } from '@angular/core';

@Component({
  selector: 'app-cart',
  standalone: false,
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css'
})
export class CartComponent {
  cartItems = [
    { id: 1, name: 'هاتف ذكي Pro Max', price: 12500, quantity: 1, image: 'https://via.placeholder.com/100x100?text=Phone' },
    { id: 2, name: 'سماعات لاسلكية', price: 2500, quantity: 2, image: 'https://via.placeholder.com/100x100?text=Headphones' },
    { id: 3, name: 'حقيبة يد فاخرة', price: 3500, quantity: 1, image: 'https://via.placeholder.com/100x100?text=Bag' }
  ];

  shippingCost = 0;
  taxRate = 0.14;

  get subtotal() {
    return this.cartItems.reduce((sum, item) => sum + (item.price * item.quantity), 0);
  }

  get tax() {
    return this.subtotal * this.taxRate;
  }

  get total() {
    return this.subtotal + this.tax + this.shippingCost;
  }

  updateQuantity(itemId: number, quantity: number) {
    const item = this.cartItems.find(i => i.id === itemId);
    if (item && quantity > 0) {
      item.quantity = quantity;
    }
  }

  removeItem(itemId: number) {
    this.cartItems = this.cartItems.filter(i => i.id !== itemId);
  }

  applyCoupon() {
    // No logic
  }

  calculateShipping(method: string) {
    if (method === 'free') this.shippingCost = this.subtotal >= 500 ? 0 : 0;
    else if (method === 'standard') this.shippingCost = 50;
    else if (method === 'express') this.shippingCost = 100;
  }
}
