import { Component, Input, OnInit } from '@angular/core';
import { CartService } from 'src/app/services/Product/cart.service';
import { ProductInterfaces } from '../Product-Page/product.interfaces';
import { AuthenticatorService } from 'src/app/services/Auth/authenticator.service';
import { FormBuilder, Validators } from '@angular/forms';
import { HttpHeaders } from '@angular/common/http';
import { OrderInterfaces, address } from './orderInterfaces';
import { OrderService } from 'src/app/services/Order/order.service';

@Component({
  selector: 'cs-order-page',
  templateUrl: './order-page.component.html',
  styleUrls: ['./order-page.component.css'],
})
export class OrderPageComponent implements OnInit {
  @Input() cartProductsFromOrderpage: ProductInterfaces[];
  @Input() isSubmitted = false;
  private productFromCart: ProductInterfaces[];
  totalPrice: number = 0;
  constructor(
    private fb: FormBuilder,
    private cartService: CartService,
    public auth: AuthenticatorService,
    public orderServices: OrderService,
  ) {}

  orderForm = this.fb.group({
    region: [
      '',
      [Validators.required, Validators.pattern(/(?<![0-9])[a-zA-Z]+(?![0-9])/)],
    ],
    country: [
      '',
      [Validators.required, Validators.pattern(/(?<![0-9])[a-zA-Z]+(?![0-9])/)],
    ],
    address: ['', Validators.required],
    postCode: [
      '',
      [
        Validators.required,
        Validators.pattern(/(?<![a-zA-Z])\d+\b(?![a-zA-Z])/),
      ],
    ],
    telephone: [
      '',
      [
        Validators.required,
        Validators.pattern(/(?<![a-zA-Z])\d+\b(?![a-zA-Z])/),
      ],
    ],
  });

  ngOnInit() {
    this.getProducts();
    this.totalPriceProduct();

    this.cartService.getProducts().subscribe((p) => (this.productFromCart = p));
  }
  getProducts() {
    this.cartService
      .getProducts()
      .subscribe((products) => (this.cartProductsFromOrderpage = products));
  }


  totalPriceProduct() {
    this.cartService.totalPrices$.subscribe((total) => {
      this.totalPrice = total;
    });
  }

  removeProduct(product: ProductInterfaces) {
    this.cartService.removeFromCart(product);
    //this.orderServices.updateOrder()
  }

  onSubmit(): void {
    console.log('form', this.orderForm.value, this.orderForm.valid);

    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Content-Length': '<calculated when request is sent>',
      'User-Agent': 'PostmanRuntime/7.33.0',
      'Accept-Encoding': 'gzip, deflate, br',
      Connection: 'keep-alive',
    });

    const addressOrder: address = {
      street: this.orderForm.get('address')?.value ?? '',
      city: this.orderForm.get('City')?.value ?? '',
      region: this.orderForm.get('region')?.value ?? '',
      state: this.orderForm.get('region')?.value ?? '',// change to telephone number in .net
      postalCode: this.orderForm.get('postCode')?.value ?? '',
      country: this.orderForm.get('Country')?.value ?? '',
    };

    const requestBody: OrderInterfaces = {
      products: this.productFromCart,
      address: addressOrder,
      totalPrices: this.totalPrice,
      currency: "$",
      status: 1, 
      userId: "id",
    };
    this.orderServices.addOrder(requestBody);
    this.isSubmitted= this.orderServices.isSubmitted;
     
  }

  onUserInput(event: any) {
    let inputText = event.target.value;
    this.isSubmitted = inputText === '';
  }

  validationField(fieldname: string): string {
    const control = this.orderForm.get(fieldname);

    if (control?.invalid && (control?.dirty || control?.touched)) {
      return 'invalid';
    }
    if (control?.valid) {
      return 'valid';
    }
    return 'normal';
  }

  MessagePlaceholder(labelname: string, placeholder: string): string {
    return this.orderForm.get(labelname)?.invalid &&
      (this.orderForm.get(labelname)?.dirty ||
        this.orderForm.get(labelname)?.touched)
      ? ' Required'
      : placeholder;
  }
}
