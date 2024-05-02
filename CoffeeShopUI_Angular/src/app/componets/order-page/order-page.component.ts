import { Component, Input, OnInit } from '@angular/core';
import { CartService } from 'src/app/services/Product/cart.service';
import { ProductInterfaces } from '../Product-Page/product.interfaces';
import { AuthenticatorService } from 'src/app/services/Auth/authenticator.service';
import { FormBuilder, Validators } from '@angular/forms';
import { HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'cs-order-page',
  templateUrl: './order-page.component.html',
  styleUrls: ['./order-page.component.css'],
})
export class OrderPageComponent implements OnInit {
  @Input() cartProductsFromOrderpage: ProductInterfaces[];
  @Input() isSubmitted = true;

  totalPrice: number = 0;
  constructor(
    private fb: FormBuilder,
    private cartService: CartService,
    public auth: AuthenticatorService
  ) {}

  signInForm = this.fb.group({
   
    region: ['', [Validators.required, Validators.pattern(/(?<![0-9])[a-zA-Z]+(?![0-9])/)]],
    country: ['', [Validators.required, Validators.pattern(/(?<![0-9])[a-zA-Z]+(?![0-9])/)]],
    address: ['', Validators.required],
    postCode: ['', [Validators.required, Validators.pattern(/(?<![a-zA-Z])\d+\b(?![a-zA-Z])/)]],
    telephone: ['', [Validators.required, Validators.pattern(/(?<![a-zA-Z])\d+\b(?![a-zA-Z])/)]],
  });
  ngOnInit() {
    this.getProducts();
    this.totalPriceProduct();
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
  }
  onSubmit(): void {
    console.log(
      'signUpForm form',
      this.signInForm.value,
      this.signInForm.valid
    );
    // const headers = new HttpHeaders({
    //   'Content-Type': 'application/json',
    //   'Content-Length': '<calculated when request is sent>',
    //   'User-Agent': 'PostmanRuntime/7.33.0',
    //   'Accept-Encoding': 'gzip, deflate, br',
    //   Connection: 'keep-alive',
    // });

    // const requestBody = {
    
    // };
    // this.auth.login(requestBody, this.signInForm);
    // this.isSubmitted = this.auth.isSubmitted;
  }
  onUserInput(event: any) {
    let inputText = event.target.value;
    this.isSubmitted = inputText === '';
  }

  validationField(fieldname: string): string {
    const control = this.signInForm.get(fieldname);

    if (control?.invalid && (control?.dirty || control?.touched)) {
      return 'invalid';
    }
    if (control?.valid) {
      return 'valid';
    }
    return 'normal';
  }
  MessagePlaceholder(labelname: string, placeholder: string): string {
    return this.signInForm.get(labelname)?.invalid &&
      (this.signInForm.get(labelname)?.dirty ||
        this.signInForm.get(labelname)?.touched)
      ? ' Required'
      : placeholder;
  }
  
}
