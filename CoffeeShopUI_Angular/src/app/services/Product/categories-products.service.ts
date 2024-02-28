import { Injectable } from '@angular/core';
import {CategoryInterfaces} from 'src/app/componets/Product-Page/product.interfaces';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class CategoriesProductsService {

  private apiUrl = 'https://localhost:7282/api/Product/GetCategories';

  constructor(private http: HttpClient) {}

  getCategories(): Observable<CategoryInterfaces[]> {
    return this.http.get<CategoryInterfaces[]>(`${this.apiUrl}`);
  }
  
}
