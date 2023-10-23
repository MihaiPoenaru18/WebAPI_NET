import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'navbar_header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit {
  hamburgerVariabel: boolean = true;
  menu_icon_variable: boolean = false;
  menuType: String = 'Home';
  constructor(private route: Router) {}

  openMenu() {
    if (!this.hamburgerVariabel) {
      this.hamburgerVariabel = true;
    } else {
      if (this.hamburgerVariabel) this.hamburgerVariabel = false;
    }

    if (!this.menu_icon_variable) {
      this.menu_icon_variable = true;
    } else {
      if (this.menu_icon_variable) this.menu_icon_variable = false;
    }
  }
  ngOnInit(): void {
    this.route.events.subscribe((val: any) => {
      
      if (val.url) {
        if (val.url.includes('sign-up') || val.url.includes('sign-in')) {
           this.menuType = 'sign-up';
          console.warn("sign-up!")
        } else {
          console.warn("home")
          this.menuType = 'Home';
        }
      }
    });
  }
}
