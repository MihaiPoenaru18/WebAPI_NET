import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ProductInterfaces } from '../../Product-Page/product.interfaces';
import { CartService } from 'src/app/services/Product/cart.service';

@Component({
  selector: 'cs-cart-dropdown',
  templateUrl: './cart-dropdown.component.html',
  styleUrls: ['./cart-dropdown.component.css'],
})
export class CartDropdownComponent {
  @Input() cartDropDownProducts: ProductInterfaces[];
  @Input()showCart: boolean = false;
  @Output() eventShowProductFromCart= new EventEmitter<ProductInterfaces[]>();
}
