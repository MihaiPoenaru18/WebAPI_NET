import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SvgFooterComponent } from './svg-footer.component';

describe('SvgFooterComponent', () => {
  let component: SvgFooterComponent;
  let fixture: ComponentFixture<SvgFooterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SvgFooterComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SvgFooterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
