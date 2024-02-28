import { Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { ProductInterfaces } from 'src/app/componets/Product-Page/products-list/product.interfaces';
import { ProductConversionTypeService } from './product-conversion-type.service';

@Injectable({
  providedIn: 'root'
})
export class ProductsService  {

  private apiUrl = 'https://localhost:7282/api/Product/GetProducts';

  constructor(private http: HttpClient, private converter: ProductConversionTypeService) {}

  getProducts(): Observable<ProductInterfaces[]> {
    //TODO rewrite with conversion usage

    return this.http.get<any[]>(`${this.apiUrl}`)
        .pipe(
          map(products=> {return this.converter.convertProductsListFromBEtoFE(products) } )
        );

  }
  
  // addProduct(newProduct: ProductInterfaces): Observable<ProductInterfaces> {
  //   // Assuming your API supports adding a new product
  //   return this.http.post<ProductInterfaces>(`${this.apiUrl}/products`, newProduct);
  // }
}
