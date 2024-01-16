import { Component, OnInit } from '@angular/core';
import { ProductInterfaces } from './product.interfaces';
import { ProductsService } from 'src/app//services/Product/products.service';

@Component({
  selector: 'cs-products-list',
  templateUrl: './products-list.component.html',
  styleUrls: ['./products-list.component.css'],
  providers: [ProductsService],
})
export class ProductsListComponent implements OnInit {
  products: ProductInterfaces[] = [];
  constructor(private productService: ProductsService) {}

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts() {
    this.productService.getProducts().subscribe((data) => {
      this.products = data;
    });
  }
}
