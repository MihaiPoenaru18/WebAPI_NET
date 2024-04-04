import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'cs-product-quantity-counter',
  templateUrl: './product-quantity-counter.component.html',
  styleUrls: ['./product-quantity-counter.component.css'],
})
export class ProductQuantityCounterComponent {
  quantity: number = 1;
  @Input() quantityAvailable: any = 0;
  @Output() eventCountQuantity = new EventEmitter<number>();
  increment() {
    if (this.quantityAvailable - 1 >= this.quantity) {
      this.quantity++;
    }
    this.getQuantity();
  }

  decrement() {
    if (this.quantity > 1) {
      this.quantity--;
    }
    this.getQuantity()
  }
  getQuantity(){
    this.eventCountQuantity.emit(this.quantity);
  }
}
