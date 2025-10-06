import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

import { ProductResponse, PagedProductResponse } from '../../core/models/product.model';
import { ProductService } from '../../core/services/product.service';
import { Subject, takeUntil, map, tap, catchError, EMPTY, combineLatest, debounceTime, distinctUntilChanged } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { BehaviorSubject, switchMap } from 'rxjs';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-employee-dashboard',
  imports: [CommonModule, RouterLink],
  standalone: true,
  templateUrl: './employee-dashboard.html',
  styleUrl: './employee-dashboard.css'
})

export class EmployeeDashboard implements OnInit, OnDestroy {

  public products: ProductResponse[] = [];    
  public error: string | null = null;
  public serverUrl = environment.apiUrl;

  private destroy$ = new Subject<void>();

  public totalCount = 0;
  public pageSize = 4;
  public currentPage = 1;

  private sortCriteria$ = new BehaviorSubject<{ sortBy: string, sortDirection: string }>({ sortBy: '', sortDirection: '' });
  private searchTerm$ = new BehaviorSubject<string>('');
  private currentPage$ = new BehaviorSubject<number>(1);

  constructor(private productService: ProductService) { }

  ngOnInit(): void {
    combineLatest([
      this.sortCriteria$,
      this.searchTerm$.pipe(debounceTime(300), distinctUntilChanged()),
      this.currentPage$ 
    ]).pipe(
      switchMap(([criteria, term, page]) => {
        this.currentPage = page; 
        return this.productService.getAllProducts(criteria.sortBy, criteria.sortDirection, term, page, this.pageSize).pipe(
          catchError((err: HttpErrorResponse) => {
            this.error = 'An error occurred while retrieving the product.';
            return EMPTY; 
          })
        );
      }),
      tap(response => {
        this.products = response.items;
        this.totalCount = response.totalCount;
      }),
      takeUntil(this.destroy$)
    ).subscribe();
  }

  onPageChange(newPage: number): void {
    if (newPage > 0 && newPage <= this.totalPages) {
      this.currentPage$.next(newPage);
    }
  }

  get totalPages(): number {
    return Math.ceil(this.totalCount / this.pageSize);
  }

  onSortChange(event: Event): void {
    const value = (event.target as HTMLSelectElement).value;
    if (value) {
      const [sortBy, sortDirection] = value.split('-'); 
      this.sortCriteria$.next({ sortBy, sortDirection }); 
    } else {
      this.sortCriteria$.next({ sortBy: '', sortDirection: '' });
    }
  }

  onSearchChange(event: Event): void {
    const value = (event.target as HTMLInputElement).value;
    this.currentPage$.next(1); //dodao
    this.searchTerm$.next(value);
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}