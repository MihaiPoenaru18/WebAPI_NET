import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ProductsService } from 'src/app/services/Product/products.service';
import { ProductInterfaces } from '../product.interfaces';

@Component({
  selector: 'cs-navbar-products-list',
  templateUrl: './navbar-products-list.component.html',
  styleUrls: ['./navbar-products-list.component.css'],
  providers: [ProductsService],
})
export class NavbarProductsListComponent {
  @Output() showCategoryChange = new EventEmitter<boolean>();
  @Output() searchEvent = new EventEmitter<any>();
  isShowCategory: boolean = false;
  listName: string = 'Categories';
  searchTerm: string;
  orderType: string = 'asc';
  orderBy: any;
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
      quantity: 2,
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

  constructor(private productService: ProductsService) {}

  showCategories() {
    this.listName = 'Products';
    this.isShowCategory = !this.isShowCategory;
    this.showCategoryChange.emit(this.isShowCategory);

    this.updateListName();
    console.log('isShowCategory = ' + this.isShowCategory);
  }

  updateListName() {
    if (!this.isShowCategory) {
      this.listName = 'Categories';
    }
  }
  setSearchTerm(event: any) {
    event.preventDefault();
    this.searchTerm = event.target.value.trim();
  }

  onSearch() {
    console.log('serach !!! ' + 'term = ' + this.searchTerm);
    this.productService.searchProducts(this.searchTerm).subscribe(
      (searchedProducts) => {
        this.searchEvent.emit(searchedProducts);
      },
      (error) => {
        console.error('Error fetching onSearch() products by a term', error);
      }
    );
  }
  onSearchEnter(event: any): void {
    event.preventDefault();
    // Perform the search operation when Enter key is pressed
    this.productService.searchProducts(this.searchTerm).subscribe(
      (searchedProducts) => {
        this.searchEvent.emit(searchedProducts);
      },
      (error) => {
        console.error(
          'Error fetching onSearchEnter() products by a term:',
          error
        );
      }
    );
  }
  refreshPage() {
    window.location.reload();
  }
  setOrderType(type: string) {
    this.orderType = type;
    console.log('t =' + type);
  }
  setOrderBy(order: string) {
    console.log('Selected order by: 22' + this.orderBy);
    this.orderBy = order;
    this.onOrderByChange()
  }
  onOrderByChange() {
    console.log('Selected order by: ' + this.orderBy);
   
    this.productService
      .sortProductByTerm(this.orderBy, this.orderType)
      .subscribe(
        (sortProducts) => {
          this.searchEvent.emit(sortProducts);
        },
        (error) => {
          this.sortProducts();
          console.error(
            'Error fetching onOrderByChange() products by a term:',
            error
          );
        
        }
      );
  }
  sortProducts(): void {
    switch (this.orderBy) {
      case 'Name':
        this.products.sort((a, b) => a.name.localeCompare(b.name));
        break;
      case 'Price':
        this.products.sort((a, b) => a.price - b.price);
        break;
      case 'Category':
        this.products.sort((a, b) =>
          a.category.name.localeCompare(b.category.name)
        );
        break;
      default:
        this.products.sort((a, b) => a.name.localeCompare(b.name));
        break;
    }
  }
}
