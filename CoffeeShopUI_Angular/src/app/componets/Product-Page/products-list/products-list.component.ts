import { Output, Component, OnInit, EventEmitter, Input } from '@angular/core';
import { ProductInterfaces } from '../product.interfaces';
import { ProductsService } from 'src/app//services/Product/products.service';
import { ChangeDetectorRef } from '@angular/core';
import { EmailValidator } from '@angular/forms';
@Component({
  selector: 'cs-products-list',
  templateUrl: './products-list.component.html',
  styleUrls: ['./products-list.component.css'],
  providers: [ProductsService],
})
export class ProductsListComponent implements OnInit {
  isShowCategory: any;
  constructor(
    private productService: ProductsService,
    private cdr: ChangeDetectorRef
  ) {}

  @Output() showCategory: boolean = false;
  @Output() categoryName: string = '';

   products: ProductInterfaces[] = [
    {
      name: 'Tea',
      sku: 'SKU123',
      description: 'Description of Product C',
      currency: 'USD',
      price: 5,
      quantity: 2,
      isStock: true,
      promotion: {
        pricePromotion: 15,
        startDate: '2024.02.01',
        endDate: '2024.02.06',
      },
      category: {
        name: 'Category A',
        imagePath: 'p',
      },
      imagePath: '/assets/images/products/pack.jpg',
    },
    {
      name: 'Product C',
      sku: 'SKU123',
      description: 'Description of Product C',
      currency: 'USD',
      price: 10,
      quantity: 2,
      isStock: true,
      promotion: {
        pricePromotion: 15,
        startDate: '2024.02.01',
        endDate: '2024.02.06',
      },
      category: {
        name: 'Category B',
        imagePath: 'p',
      },
      imagePath: '/assets/images/products/pack 4.jpg',
    },
    {
      name: 'Coffee',
      sku: 'SKU123',
      description: 'Description of Product C',
      currency: 'USD',
      price: 20,
      quantity: 2,
      isStock: true,
      promotion: {
        pricePromotion: 15,
        startDate: '2024.02.01',
        endDate: '2024.02.06',
      },
      category: {
        name: 'Category B',
        imagePath: 'p',
      },
      imagePath: '/assets/images/products/Pack3.jpg',
    },
    {
      name: 'Cake',
      sku: 'SKU123',
      description: 'Description of Product C',
      currency: 'USD',
      price: 25,
      quantity: 8,
      isStock: true,
      promotion: {
        pricePromotion: 15,
        startDate: '2024.02.01',
        endDate: '2024.02.06',
      },
      category: {
        name: 'Category c',
        imagePath: 'p',
      },
      imagePath: '/assets/images/products/pack.jpg',
    },
  ];

  ngOnInit(): void {
    this.loadProducts();
    console.log(this.categoryName + '-- lenght =' + this.categoryName.length);
  }

  loadProducts() {
    this.productService.getProducts().subscribe(
      (jsonResponse) => {
        console.log('Received data:', jsonResponse);
        this.products = [...this.products, ...jsonResponse];
        console.log('Received Products:', this.products);
        this.cdr.detectChanges();
      },
      (error) => {
        console.error('Error fetching products:', error);
      }
    );
  }

  onShowCategoryChange(isShowCategory: boolean) {
    this.showCategory = isShowCategory;
  }

  onShowProductsByCategory(name: string) {
    this.categoryName = name;
  }

  filterProductsByCategory(): ProductInterfaces[] {
    if (this.categoryName.length == 0 || this.categoryName == null) {
      return this.products;
    }
    console.log(
      this.products.filter(
        (product) => product.category.name === this.categoryName
      )
    );
    return this.products.filter(
      (product) => product.category.name === this.categoryName
    );
  }

  filterProductsBySearchTerm(searchProducts: ProductInterfaces[]) {
    this.products = searchProducts;
  }

  sortProductsBy(orderProducts: ProductInterfaces[]) {
    this.products = orderProducts;
  }
   getProductOnClick(product:ProductInterfaces) {
    
    localStorage.clear;
    const productData: { [key: string]: string } = {
      'Product Name': product.name,
      'SKU': product.sku,
      'Description': product.description,
      'Currency': product.currency,
      'Price': product.price.toString(),
      'Quantity': product.quantity.toString(),
      'IsStock': product.isStock.toString(),
      'ImagePath': product.imagePath,
      'Category': product.category.name,
      'Price Promotion': product.promotion ? product.promotion.pricePromotion.toString() : '',
      'Start Date': product.promotion ? product.promotion.startDate : '',
      'End Date': product.promotion ? product.promotion.endDate : ''
    };
  
    for (const key in productData) {
      if (Object.prototype.hasOwnProperty.call(productData, key)) {
        localStorage.setItem(key, productData[key]);
      }
    }
  
    return new Set(Object.values(productData).map(String));
  }
}
