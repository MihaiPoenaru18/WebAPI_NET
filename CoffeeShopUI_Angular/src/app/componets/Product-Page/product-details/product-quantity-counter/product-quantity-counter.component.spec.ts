import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductQuantityCounterComponent } from './product-quantity-counter.component';

describe('ProductQuantityCounterComponent', () => {
  let component: ProductQuantityCounterComponent;
  let fixture: ComponentFixture<ProductQuantityCounterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProductQuantityCounterComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProductQuantityCounterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
