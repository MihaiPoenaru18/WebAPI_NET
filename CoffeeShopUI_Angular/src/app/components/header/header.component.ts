import { Component } from '@angular/core';

@Component({
  selector: 'navbar_header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {

  hamburgerVariabel : boolean = true;
  menu_icon_variable : boolean = false;
  openMenu(){

    if(!(this.hamburgerVariabel))
    {
      this.hamburgerVariabel= true;
    }else{
      if(this.hamburgerVariabel)
      this.hamburgerVariabel=false;
    }
    
    if(!(this.menu_icon_variable))
    {
      this.menu_icon_variable= true;
    }else{
      if(this.menu_icon_variable)
      this.menu_icon_variable=false;
    }
  }
}
