import { Component, Input } from '@angular/core';
import { ProductInterfaces } from '../product.interfaces';

@Component({
  selector: 'cs-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css'],
})
export class ProductDetailsComponent {
  @Input() productInfo: ProductInterfaces | null = null;
  constructor() { }

}
