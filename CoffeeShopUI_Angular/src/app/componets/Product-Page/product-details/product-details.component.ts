import { Component, Input, OnInit, Output } from '@angular/core';
import { ProductInterfaces } from '../product.interfaces';
import { CartService } from 'src/app/services/Product/cart.service';

@Component({
  selector: 'cs-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css'],
  providers: [CartService],
})
export class ProductDetailsComponent implements OnInit {
  @Output() productInfo: ProductInterfaces | null = null;
  constructor(public  cartSevice: CartService) {}

  ngOnInit(): void {
    this.getProductDataFromLocalStorage();
  }
  showDescription: boolean = false;
  getProductDataFromLocalStorage() {
    const productData: ProductInterfaces = {
      name: localStorage.getItem('Product Name') || '',
      sku: localStorage.getItem('SKU') || '',
      description: localStorage.getItem('Description') || '',
      currency: localStorage.getItem('Currency') || '',
      price: parseFloat(localStorage.getItem('Price') || '0'),
      quantity: parseInt(localStorage.getItem('Quantity') || '0'),
      isStock: localStorage.getItem('IsStock') === 'true',
      imagePath: localStorage.getItem('ImagePath') || '',
      promotion: {
        pricePromotion: parseFloat(
          localStorage.getItem('Price Promotion') || '0'
        ),
        startDate: localStorage.getItem('Start Date') || '',
        endDate: localStorage.getItem('End Date') || '',
      },
      category: {
        name: localStorage.getItem('Category') || '',
        imagePath: localStorage.getItem('Category ImagePath') || '',
      },
    };

    this.productInfo = productData;
    console.log(this.productInfo);
  }
  toggleDescription(): void {
    this.showDescription = !this.showDescription;
  }
  putTheProductToCart() {
    if (this.productInfo != null) {
      this.cartSevice.addToCart(this.productInfo);
      console.log ("putTheProductToCart product name" + this.productInfo.name + " number= " + this.cartSevice.getNumberOfProducts() )
    }
  }
}
