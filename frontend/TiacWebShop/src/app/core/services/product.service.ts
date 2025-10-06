import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { PagedProductResponse, ProductForAdmin, ProductResponse } from '../models/product.model';
import { ProductDetailsResponse } from '../models/productDetails.models';

@Injectable({
  providedIn: 'root'
})

export class ProductService {

  private PRODUCT_RESOURCE = 'products';

  constructor(private http: HttpClient) { }

  public getAllProducts(sortBy?: string, sortDirection?: string, name?: string, pageNumber = 1, pageSize = 5): Observable<PagedProductResponse> {
    let params = new HttpParams();
    if (sortBy && sortDirection) {
      params = params.append('SortBy', sortBy);
      params = params.append('SortDirection', sortDirection);
    }

    if (name) {
      params = params.append('Name', name);
    }

    params = params.append('PageNumber', pageNumber.toString());
    params = params.append('PageSize', pageSize.toString());

    return this.http.get<PagedProductResponse>(`${environment.apiUrl}/${this.PRODUCT_RESOURCE}/getAll`, { params });
  }

  public getAllProductsForAdmin(): Observable<ProductForAdmin[]> {
    return this.http.get<ProductForAdmin[]>(`${environment.apiUrl}/products/getAllForAdmin`);
  }

  public getProductDetails(id: number): Observable<ProductDetailsResponse> {
    return this.http.get<ProductDetailsResponse>(`${environment.apiUrl}/products/${id}/details`);
  }
}
