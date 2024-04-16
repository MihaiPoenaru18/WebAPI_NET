import { Component, Input, OnInit } from '@angular/core';
import { CartService } from 'src/app/services/Product/cart.service';
import { ProductInterfaces } from '../Product-Page/product.interfaces';

@Component({
  selector: 'cs-order-page',
  templateUrl: './order-page.component.html',
  styleUrls: ['./order-page.component.css'],
})
export class OrderPageComponent implements OnInit {
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
