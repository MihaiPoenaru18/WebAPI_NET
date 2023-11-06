import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'cs-navbar',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit {
  hamburgerVariabel: boolean = true;
  menuIconVariabel: boolean = false;
  menuType: String = 'Home';
  constructor(private route: Router) {}

  openMenu() {
    if (!this.hamburgerVariabel) {
      this.hamburgerVariabel = true;
    } else {
      if (this.hamburgerVariabel) this.hamburgerVariabel = false;
    }

    if (!this. menuIconVariabel) {
      this. menuIconVariabel = true;
    } else {
      if (this. menuIconVariabel) this. menuIconVariabel = false;
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
