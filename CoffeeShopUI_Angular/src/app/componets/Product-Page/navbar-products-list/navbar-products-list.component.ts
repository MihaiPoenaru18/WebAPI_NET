import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'cs-navbar-products-list',
  templateUrl: './navbar-products-list.component.html',
  styleUrls: ['./navbar-products-list.component.css'],
})
export class NavbarProductsListComponent {
  @Output() showCategoryChange = new EventEmitter<boolean>();
  isShowCategory: boolean = false;
  listName: string = 'Categories';
  
  showDescription() {
    this.listName = 'Products';
    this.isShowCategory = !this.isShowCategory;
    this.showCategoryChange.emit(this.isShowCategory);
    this.updateListName();
    console.log('isShowCategory = ' + this.isShowCategory);
  }

  private updateListName() {
    if (!this.isShowCategory) {
      this.listName = 'Categories';
    }
  }
}
