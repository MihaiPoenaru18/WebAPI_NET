import { Component, Input } from '@angular/core';
import { ProductInterfaces } from '../../Product-Page/product.interfaces';

@Component({
  selector: 'cs-cart-dropdown',
  templateUrl: './cart-dropdown.component.html',
  styleUrls: ['./cart-dropdown.component.css']
})
export class CartDropdownComponent {
 @Input() cartProducts: ProductInterfaces[];
 @Input()showCart: boolean = false;
}
