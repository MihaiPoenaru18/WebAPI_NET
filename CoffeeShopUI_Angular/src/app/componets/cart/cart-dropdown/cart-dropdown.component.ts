import { Component, Input, OnInit } from '@angular/core';
import { ProductInterfaces } from '../../Product-Page/product.interfaces';
import { CartService } from 'src/app/services/Product/cart.service';

@Component({
  selector: 'cs-cart-dropdown',
  templateUrl: './cart-dropdown.component.html',
  styleUrls: ['./cart-dropdown.component.css'],
})
export class CartDropdownComponent implements OnInit{
  @Input() cartProducts: ProductInterfaces[];
  @Input()showCart: boolean = false;
  totalPrice: number = 0;
  constructor(private cartService: CartService) {}

  
  ngOnInit() {
    this.totalPriceProduct();
  }

  totalPriceProduct() {
    this.cartService.totalPrices$.subscribe(total => {
      this.totalPrice = total;
    });
    console.log("T"+ this.totalPrice)
  }
  removeProduct(product: ProductInterfaces){
    this.cartService.removeFromCart(product);
  }
}
