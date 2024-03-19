import { Component,Input } from '@angular/core';

@Component({
  selector: 'cs-product-quantity-counter',
  templateUrl: './product-quantity-counter.component.html',
  styleUrls: ['./product-quantity-counter.component.css']
})
export class ProductQuantityCounterComponent {
  @Input() quantity: number = 1;

  increment() {
    this.quantity++;
  }

  decrement() {
    if (this.quantity > 0) {
      this.quantity--;
    }
  }
}
