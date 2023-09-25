import { Component } from '@angular/core';
import { SlideInterface } from './components/slider.interfaces';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'CoffeeShopUI_Angular';
  slides: SlideInterface[] = [
    { url: '/assets/images/img1.png', title: 'beach' },
    { url: '/assets/images/img2.jpg', title: 'boat' },
    { url: '/assets/images/img3.jpg', title: 'forest' },
    { url: '/assets/images/img4.jpg', title: 'city' },
    { url: '/assets/images/img5.jpg', title: 'italy' },
  ];
}