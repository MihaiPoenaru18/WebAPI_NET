import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NavbarProductsListComponent } from './navbar-products-list.component';

describe('NavbarProductsListComponent', () => {
  let component: NavbarProductsListComponent;
  let fixture: ComponentFixture<NavbarProductsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NavbarProductsListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NavbarProductsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
