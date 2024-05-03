import { HttpClient } from '@angular/common/http';
import { Injectable, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';
import { OrderInterfaces } from 'src/app/componets/order-page/orderInterfaces';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  private apiUrl = 'https://localhost:7282/api/Order';
  @Input() isSubmitted = false;

  constructor(private http: HttpClient) {}

  addOrder(newOrder: OrderInterfaces): Observable<OrderInterfaces> {
    return this.http.post<OrderInterfaces>(`${this.apiUrl}/AddOrder`, newOrder);
  }

  getAllOrders(): Observable<OrderInterfaces[]> {
    return this.http.get<OrderInterfaces[]>(`${this.apiUrl}/GetAllOrder`);
  }

  updateOrder(updateOrder: OrderInterfaces, form: FormGroup) {
    this.http.post<any>(`${this.apiUrl}/UpdateOrder`, updateOrder).subscribe({
      next: (response) => {
        console.log('POST request successful', response);
        form.reset();
        this.isSubmitted = true;
      },
      error: (error) => {
        console.error('POST request failed', error);
        if (error.status === 400) {
          console.error('Bad Request:', error.error);
        } else {
          console.error('An unexpected error occurred:', error);
        }
      },
    });
  }
  
}
