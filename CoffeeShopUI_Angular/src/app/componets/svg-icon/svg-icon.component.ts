import { Component, Input } from '@angular/core';

@Component({
  selector: 'cs-svg-icon',
  templateUrl: './svg-icon.component.html',
  styleUrls: ['./svg-icon.component.css']
})
export class SvgIconComponent {
  @Input() svgName: string;
}
