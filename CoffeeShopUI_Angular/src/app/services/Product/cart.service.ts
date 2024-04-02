import { Injectable } from '@angular/core';
import { ProductInterfaces } from 'src/app/componets/Product-Page/product.interfaces';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  private products: ProductInterfaces[] = [];

  constructor() { }

  getProducts(): ProductInterfaces[] {
    return this.products;
  }

  addToCart(product: ProductInterfaces): void {
    this.products.push(product);
    console.log("product "+ this.products)
  }

  removeFromCart(product: ProductInterfaces): void {
    const index = this.products.indexOf(product);
    if (index !== -1) {
      this.products.splice(index, 1);
    }
  }

  getNumberOfProducts(): number {
    return this.products.length;
  }
}