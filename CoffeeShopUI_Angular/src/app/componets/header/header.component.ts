import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthenticatorService } from 'src/app/services/Auth/authenticator.service';
import { CartService } from 'src/app/services/Product/cart.service';
import { ProductInterfaces } from '../Product-Page/product.interfaces';

@Component({
  selector: 'cs-navbar',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
  providers: [AuthenticatorService],
})
export class HeaderComponent implements OnInit {
  hamburgerVariabel: boolean = true;
  menuIconVariabel: boolean = false;
  menuType: string = 'Home';
  numberOfProductsFromCart = 0;
  constructor(private route: Router, public auth: AuthenticatorService) {}

  openMenu() {
    this.hamburgerVariabel = !this.hamburgerVariabel;
    this.menuIconVariabel = !this.menuIconVariabel;
  }
  ngOnInit(): void {
    this.route.events.subscribe((val: any) => {
      if (val.url) {
        this.menuType =
          val.url.includes('sign-up') || val.url.includes('sign-in')
            ? 'sign-up'
            : 'Home';
        console.warn(this.menuType === 'sign-up' ? 'sign-up!' : 'home'); // Simplified logging
      }
    });
  }
  showCartIcon(numberOfProducts:number){
    this.numberOfProductsFromCart = numberOfProducts;
  }
}
