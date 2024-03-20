import { Component, Input } from '@angular/core';

@Component({
  selector: 'cs-product-description',
  templateUrl: './product-description.component.html',
  styleUrls: ['./product-description.component.css']
})
export class ProductDescriptionComponent {
  @Input() description: any;
}
