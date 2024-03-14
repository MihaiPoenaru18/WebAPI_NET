import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ProductsService } from 'src/app/services/Product/products.service';

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
  orderType:string = 'asc';
  orderBy:any;
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
        console.error('Error fetching onSearchEnter() products by a term:', error);
      }
    );
  }
  refreshPage() {
    window.location.reload();
  }
  setOrderType(type:string){
    this.orderType = type;
    console.log("t ="+type )
  }
  onOrderByChange(){
    console.log("Selected order by: " + this.orderBy)
    this.productService.sortProductByTerm(this.orderBy, this.orderType).subscribe(
    (sortProducts) => {
      this.searchEvent.emit(sortProducts);
    },
    (error) => {
      console.error('Error fetching onOrderByChange() products by a term:', error);
    }
  );
  }
}
