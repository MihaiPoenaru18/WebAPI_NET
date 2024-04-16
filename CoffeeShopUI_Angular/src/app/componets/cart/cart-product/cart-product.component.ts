import { Component, Input, OnInit } from '@angular/core';
import { ProductInterfaces } from '../../Product-Page/product.interfaces';
import { CartService } from 'src/app/services/Product/cart.service';

@Component({
  selector: 'cs-cart-product',
  templateUrl: './cart-product.component.html',
  styleUrls: ['./cart-product.component.css'],
})
export class CartProductComponent implements OnInit {
  @Input() cartProducts: ProductInterfaces[];
  totalPrice: number = 0;
  constructor(private cartService: CartService) {}

  ngOnInit() {
    this.totalPriceProduct();
  }

  totalPriceProduct() {
    this.cartService.totalPrices$.subscribe((total) => {
      this.totalPrice = total;
    });
  }
  removeProduct(product: ProductInterfaces) {
    this.cartService.removeFromCart(product);
  }
}
