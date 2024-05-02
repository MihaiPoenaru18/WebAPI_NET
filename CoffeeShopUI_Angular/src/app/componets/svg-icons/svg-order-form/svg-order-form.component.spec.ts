import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SvgOrderFormComponent } from './svg-order-form.component';

describe('SvgOrderFormComponent', () => {
  let component: SvgOrderFormComponent;
  let fixture: ComponentFixture<SvgOrderFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SvgOrderFormComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SvgOrderFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
