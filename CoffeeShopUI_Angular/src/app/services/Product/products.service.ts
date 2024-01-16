import { Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ProductInterfaces } from 'src/app/componets/Product-Page/products-list/product.interfaces';

@Injectable({
  providedIn: 'root'
})
export class ProductsService  {

  private apiUrl = 'https://localhost:7282/api/Product/GetProducts';

  constructor(private http: HttpClient) {}

  getProducts(): Observable<ProductInterfaces[]> {
    return this.http.get<ProductInterfaces[]>(`${this.apiUrl}`);
  }

  // addProduct(newProduct: ProductInterfaces): Observable<ProductInterfaces> {
  //   // Assuming your API supports adding a new product
  //   return this.http.post<ProductInterfaces>(`${this.apiUrl}/products`, newProduct);
  // }
}
