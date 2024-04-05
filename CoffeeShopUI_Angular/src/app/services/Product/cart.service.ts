import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ProductInterfaces } from 'src/app/componets/Product-Page/product.interfaces';
@Injectable({
  providedIn: 'root',
})
export class CartService {
  public productList: ProductInterfaces[] = [];
  private cartItemList: BehaviorSubject<ProductInterfaces[]> =
    new BehaviorSubject<ProductInterfaces[]>([]);

  constructor() {}

  getProducts() {
    return this.cartItemList.asObservable();
  }

  setProduct(product: ProductInterfaces[]) {
    this.productList.push(...product);
    this.cartItemList.next(product);
  }

  addToCart(product: ProductInterfaces) {
    const existingProductIndex = this.productList.findIndex(
      (item) => item.name === product.name
    );

    if (existingProductIndex !== -1 && existingProductIndex <= product.quantity) {
      this.productList[existingProductIndex].quantity += product.quantity;
    } else {
      this.productList.push(product);
    }

    this.cartItemList.next(this.productList);
    console.log('cart item length= ' + this.productList.length);
  }

  getTotalPrice() {}

  removeFromCart(product: ProductInterfaces): void {
    const index = this.productList.indexOf(product);
    if (index !== -1) {
      this.productList.splice(index, 1);
      this.cartItemList.next([...this.productList]);
      console.log(
        'Product ' +
          product.name +
          ' removed from cart. Total products: ' +
          this.productList.length
      );
    }
  }
}
