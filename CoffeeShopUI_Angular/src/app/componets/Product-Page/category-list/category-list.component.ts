import { CategoriesProductsService } from 'src/app/services/Product/categories-products.service';
import { Component, OnInit } from '@angular/core';
import { CategoryInterfaces } from '../product.interfaces';

@Component({
  selector: 'cs-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.css'],
  providers: [CategoriesProductsService ]
})
export class CategoryListComponent implements OnInit{
 constructor(
   private categoriesService: CategoriesProductsService,
  ){}
  categories : CategoryInterfaces[] = []
  ngOnInit(): void {
    this.loadCategories();
  }

  loadCategories () {
    this.categoriesService.getCategories().subscribe(
      (jsonResponse) => {
        console.log('Received data:', jsonResponse);
        this.categories = [...this.categories, ...jsonResponse];
      },
      (error) => {
        console.error('Error fetching categories:', error);
      }
    );
  }
}
