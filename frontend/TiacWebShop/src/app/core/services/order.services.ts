import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { AllOrdersResponse, Order } from '../models/order.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OrderServices {
  private apiUrl = `${environment.apiUrl}`;

  constructor(private http: HttpClient) {
  }

  public getAllOrders(): Observable<AllOrdersResponse[]> {
    return this.http.get<AllOrdersResponse[]>(`${this.apiUrl}/orders/allOrders`);
  }

  public getOrdersHistory(): Observable<Order[]> {
    return this.http.get<Order[]>(`${this.apiUrl}/orders/history`);
  }
}
