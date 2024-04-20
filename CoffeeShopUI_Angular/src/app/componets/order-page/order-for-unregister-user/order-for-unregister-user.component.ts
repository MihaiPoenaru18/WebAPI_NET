import { Component, Input, OnInit } from '@angular/core';
import { ProductInterfaces } from '../../Product-Page/product.interfaces';
import { CartService } from 'src/app/services/Product/cart.service';

@Component({
  selector: 'cs-order-for-unregister-user',
  templateUrl: './order-for-unregister-user.component.html',
  styleUrls: ['./order-for-unregister-user.component.css']
})
export class OrderForUnregisterUserComponent implements OnInit {
  @Input() cartProductsFromOrderpage: ProductInterfaces[];
  totalPrice: number = 0;
  constructor(private cartService: CartService) {}
  ngOnInit() {
    this.getProducts();
    this.totalPriceProduct();
  }
  getProducts() {
    this.cartService
      .getProducts()
      .subscribe((products) => (this.cartProductsFromOrderpage = products));
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
