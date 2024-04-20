import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrderForRegisterUserComponent } from './order-for-register-user.component';

describe('OrderForRegisterUserComponent', () => {
  let component: OrderForRegisterUserComponent;
  let fixture: ComponentFixture<OrderForRegisterUserComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OrderForRegisterUserComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OrderForRegisterUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
