import { Component, Input, OnInit } from '@angular/core';
import { ProductInterfaces } from '../Product-Page/product.interfaces';
import { CartService } from 'src/app/services/Product/cart.service';

@Component({
  selector: 'cs-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css'],
})
export class CartComponent implements OnInit {
  constructor(private cartService: CartService) {}
  showCart: boolean = false;
  cartProducts: ProductInterfaces[] = [];
  numberOfProductInCart: number = 0;
  ngOnInit(): void {
    this.cartService.getProducts().subscribe((items) => {
      this.cartProducts = items;
      this.numberOfProductInCart = items.length;
    });
  }

  toggleCart() {
    this.showCart = !this.showCart; // Toggle the display of cart dropdown
    console.log(
      'products.length:' +
        this.cartProducts.length +
        'show cart is -' +
        this.showCart
    );
  }
  showNumberOfProducts() {
    this.numberOfProductInCart = this.cartProducts.length;
    console.log('--number = ' + this.numberOfProductInCart); // Logging observable
  }
}
