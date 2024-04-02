import { Injectable } from '@angular/core';
import { ProductInterfaces } from 'src/app/componets/Product-Page/product.interfaces';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  private products: ProductInterfaces[] = [];

  constructor() { }

  getProducts(): ProductInterfaces[] {
    console.log(this.products.length)
    return this.products;
  }

  addToCart(product: ProductInterfaces): void {
    this.products.push(product)
    
    console.log("product "+ product.name + " l= "+ this.products.length)
  }

  removeFromCart(product: ProductInterfaces): void {
    const index = this.products.indexOf(product);
    if (index !== -1) {
      this.products.splice(index, 1);
    }
  }

  getNumberOfProducts(): number {
    
    console.log("s Number "+ this.products.length )
    return this.products.length;
  }
}