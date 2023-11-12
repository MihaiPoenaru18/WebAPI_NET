import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SvgSignUpComponent } from './svg-sign-up.component';

describe('SvgSignUpComponent', () => {
  let component: SvgSignUpComponent;
  let fixture: ComponentFixture<SvgSignUpComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SvgSignUpComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SvgSignUpComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
