import { Component, OnDestroy } from '@angular/core';
import { catchError, finalize, Observable, Subject, takeUntil, tap } from 'rxjs';
import { CartResponse } from '../../core/models/cart.model';
import { environment } from '../../../environments/environment';
import { CartServices } from '../../core/services/cart.services';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-cart',
  imports: [CommonModule, RouterLink],
  templateUrl: './cart.html',
  styleUrl: './cart.css'
})

export class Cart implements OnDestroy {

  public cart$: Observable<CartResponse | null>;
  public serverUrl = environment.apiUrl;
  private destroy$ = new Subject<void>();
  public isModalVisible = false;
  public isLoading = false;

  constructor(private cartService: CartServices) {
    this.cart$ = this.cartService.cart$;
  }

  ngOnInit(): void {
    this.cartService.getCart().subscribe();
  }

  public removeItem(cartItemId: number): void {
    this.cartService.removeItem(cartItemId)
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: () => {
          alert('Item deleted successfully')
        },
        error: (err) => {
          alert('An error occurred while deleting the item')
        }
      });
  }

  public openConfirmationModal(): void {
    this.isModalVisible = true;
  }

  public onCancelOrder(): void {
    if (this.isLoading) return;
    this.isModalVisible = false;
  }

  public onConfirmOrder(): void {
    this.isLoading = true;

    this.cartService.confirmOrder().pipe(
      takeUntil(this.destroy$),
      tap((response) => {
        alert('Order successfully created');
      }),
      catchError((err) => {
        alert('An error occurred. Please try again.');
        return [];
      }),

      finalize(() => {
        this.isLoading = false;
        this.isModalVisible = false;
      }),
    ).subscribe();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
