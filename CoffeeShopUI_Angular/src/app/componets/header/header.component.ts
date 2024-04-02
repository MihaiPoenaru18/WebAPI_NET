import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticatorService } from 'src/app/services/Auth/authenticator.service';
import { CartService } from 'src/app/services/Product/cart.service';

@Component({
  selector: 'cs-navbar',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
  providers: [AuthenticatorService, CartService],
})
export class HeaderComponent implements OnInit {
  hamburgerVariabel: boolean = true;
  menuIconVariabel: boolean = false;
  menuType: String = 'Home';
  constructor(private route: Router, public auth: AuthenticatorService, public cartSevices:CartService) {}
  showCart: boolean = false; // Variable to control the display of cart dropdown
  cartProducts: any[] = [
  ];
  numberOfProductInCart:number=0;

  openMenu() {
    if (!this.hamburgerVariabel) {
      this.hamburgerVariabel = true;
    } else {
      if (this.hamburgerVariabel) this.hamburgerVariabel = false;
    }

    if (!this.menuIconVariabel) {
      this.menuIconVariabel = true;
    } else {
      if (this.menuIconVariabel) this.menuIconVariabel = false;
    }
  }
  ngOnInit(): void {
    this.route.events.subscribe((val: any) => {
      if (val.url) {
        if (val.url.includes('sign-up') || val.url.includes('sign-in')) {
          this.menuType = 'sign-up';
          console.warn('sign-up!');
        } else {
          console.warn('home');
          this.menuType = 'Home';
        }
      }
    });
   
  }
  toggleCart() {
    this.addProductToCart()
    this.showNumberOfProducts()
    this.showCart = !this.showCart; // Toggle the display of cart dropdown
  }
  showNumberOfProducts(){
    this.numberOfProductInCart = this.cartSevices.getNumberOfProducts()
    console.log("number="+ this.numberOfProductInCart)
  }
   addProductToCart(){
    this.cartProducts = this.cartSevices.getProducts();
   }
}
