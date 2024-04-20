import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrderForUnregisterUserComponent } from './order-for-unregister-user.component';

describe('OrderForUnregisterUserComponent', () => {
  let component: OrderForUnregisterUserComponent;
  let fixture: ComponentFixture<OrderForUnregisterUserComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OrderForUnregisterUserComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OrderForUnregisterUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
