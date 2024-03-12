import { Output, Component, OnInit, EventEmitter, Input } from '@angular/core';
import { ProductInterfaces } from '../product.interfaces';
import { ProductsService } from 'src/app//services/Product/products.service';
import { ChangeDetectorRef } from '@angular/core';
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


 @Output()  showCategory:boolean = false;
  products: ProductInterfaces[] = [
    {
      name: 'Product C',
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
        imagePath: 'p'
      },
      imagePath: '/assets/images/products/pack.jpg',
    },
    {
      name: 'Product C',
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
        imagePath: 'p'
      },
      imagePath: '/assets/images/products/pack 4.jpg',
    },
    {
      name: 'Product C',
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
        imagePath: 'p'
      },
      imagePath: '/assets/images/products/Pack3.jpg',
    },
    {
      name: 'Product C',
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
        imagePath: 'p'
      },
      imagePath: '/assets/images/products/pack.jpg',
    },
  ];
  

  ngOnInit(): void {
    this.loadProducts();
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
}
