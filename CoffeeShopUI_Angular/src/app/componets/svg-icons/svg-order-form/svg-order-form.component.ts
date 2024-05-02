import { Component, Input } from '@angular/core';

@Component({
  selector: 'cs-svg-order-form',
  templateUrl: './svg-order-form.component.html',
  styleUrls: ['./svg-order-form.component.css']
})
export class SvgOrderFormComponent {
  @Input() svgName: string;
  @Input() validForm: string;
  @Input() invalidForm: string;
}
