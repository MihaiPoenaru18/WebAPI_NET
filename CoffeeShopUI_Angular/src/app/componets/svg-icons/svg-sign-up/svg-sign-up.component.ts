import { Component, Input } from '@angular/core';


@Component({
  selector: 'cs-svg-sign-up',
  templateUrl: './svg-sign-up.component.html',
  styleUrls: ['./svg-sign-up.component.css']
})
export class SvgSignUpComponent {
  @Input() svgName: string;
  @Input() validForm: string;
  @Input() invalidForm: string;
}
