import { Component, OnInit } from '@angular/core';
import { ProductInterfaces } from './product.interfaces';
import { ProductsService } from 'src/app//services/Product/products.service';
import { ChangeDetectorRef } from '@angular/core';
@Component({
  selector: 'cs-products-list',
  templateUrl: './products-list.component.html',
  styleUrls: ['./products-list.component.css'],
  providers: [ProductsService],
})
export class ProductsListComponent implements OnInit {
  products: any[] = [];
  constructor(private productService: ProductsService, private cdr: ChangeDetectorRef) {}

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts() {
    this.productService.getProducts().subscribe((data) => {
      console.log('Received data:', data);
      this.products = data;
      console.log('Received Products:', this.products);
      this.cdr.detectChanges();
    },
    (error) => {
      console.error('Error fetching products:', error);
    });
  }
}
