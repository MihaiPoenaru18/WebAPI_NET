import { CategoriesProductsService } from 'src/app/services/Product/categories-products.service';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CategoryInterfaces } from '../product.interfaces';

@Component({
  selector: 'cs-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.css'],
  providers: [CategoriesProductsService],
})
export class CategoryListComponent implements OnInit {
  constructor(private categoriesService: CategoriesProductsService) {}
  @Output() categorySelected = new EventEmitter<string>();
  @Output() showProductsBycategory = new EventEmitter<boolean>();
  categoryName: string;
  isShowCategory: boolean = false;
  categories: CategoryInterfaces[] = [
    {
      name: 'Category A',
      imagePath: '/assets/images/products/pack.jpg',
    },
    {
      name: 'Category B',
      imagePath: '/assets/images/products/pack.jpg',
    },
    {
      name: 'Category c',
      imagePath: '/assets/images/products/pack.jpg',
    },
  ];
  ngOnInit(): void {
    this.loadCategories();
  }

  loadCategories() {
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
  setCategoryByUser(categoryName: any) {
    this.categoryName = categoryName;
    console.log('Name - ' + categoryName);
  }

  selectCategory() {
    this.categorySelected.emit(this.categoryName);
    this.showProductsBycategory.emit(this.isShowCategory);
    console.log(
      'Name output - ' +
        this.categoryName +
        ' isShowCategory = ' +
        this.isShowCategory
    );
  }
}
