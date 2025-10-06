import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { AddToCartRequest, CartResponse } from '../models/cart.model';
import { BehaviorSubject, concatMap, Observable, tap } from 'rxjs';
import { OrderResponse } from '../models/order.model';

@Injectable({
  providedIn: 'root'
})

export class CartServices {
  private apiUrl = `${environment.apiUrl}`;
  private cartApiUrl = `${environment.apiUrl}/cart`;
  private cartSubject = new BehaviorSubject<CartResponse | null>(null);
  public cart$ = this.cartSubject.asObservable();

  constructor(private http: HttpClient) { }

  public addItemToCart(item: AddToCartRequest): Observable<any> {
    return this.http.post(`${this.cartApiUrl}/items`, item);
  }

  public getCart(): Observable<CartResponse> {
    return this.http.get<CartResponse>(this.cartApiUrl).pipe(
      tap(cart => {
        this.cartSubject.next(cart);
      })
    );
  }

  public removeItem(cartItemId: number): Observable<any> {
    return this.http.delete(`${this.cartApiUrl}/${cartItemId}`).pipe(
      tap(() => {
        const currentCart = this.cartSubject.getValue();

        if (currentCart) {
          const updatedItems = currentCart.items.filter(item => item.cartItemId !== cartItemId);

          const updatedCart: CartResponse = {
            ...currentCart,
            items: updatedItems,
            grandTotal: updatedItems.reduce((total, item) => total + item.totalPrice, 0)
          };

          this.cartSubject.next(updatedCart);
        }
      })
    )
  }

  public confirmOrder(): Observable<any> {
    return this.http.post<OrderResponse>(`${this.apiUrl}/orders`, {}).pipe(
      concatMap(() => this.getCart())
    );
  }
}
