import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrdersentComponent } from './ordersent.component';

describe('OrdersentComponent', () => {
  let component: OrdersentComponent;
  let fixture: ComponentFixture<OrdersentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OrdersentComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OrdersentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
