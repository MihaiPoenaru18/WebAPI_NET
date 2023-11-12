import { Component,Input } from '@angular/core';

@Component({
  selector: 'cs-svg-footer',
  templateUrl: './svg-footer.component.html',
  styleUrls: ['./svg-footer.component.css']
})
export class SvgFooterComponent {
  @Input() svgName: string;
  @Input() svgCategory: string;
}
