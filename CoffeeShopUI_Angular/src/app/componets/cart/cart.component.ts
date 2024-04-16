import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ProductInterfaces } from '../Product-Page/product.interfaces';
import { CartService } from 'src/app/services/Product/cart.service';

@Component({
  selector: 'cs-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css'],
})
export class CartComponent implements OnInit {
  constructor(private cartService: CartService) {}
  cartProducts: ProductInterfaces[] = [];
  numberOfProductInCart: number = 0;
  showCart: boolean = false;
  @Output() eventShowCartIcon = new EventEmitter<number>();

  ngOnInit(): void {
    this.toggleCart()
    this.cartService.getProducts().subscribe((items) => {
      this.cartProducts = items;
      this.numberOfProductInCart = items.length;
    });
  }

  toggleCart() {
    this.showCart = !this.showCart; 
    
  }
  showNumberOfProducts() {
    this.numberOfProductInCart = this.cartProducts.length;
  }
  showTheCart(){
    this.eventShowCartIcon.emit(this.numberOfProductInCart)
  }
}
