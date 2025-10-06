import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { CreateProductResponse } from '../models/product.model';

@Injectable({
  providedIn: 'root'
})
export class CreateProductService {
  
  constructor(private http: HttpClient) { }

  public createProduct(productData: FormData) : Observable<CreateProductResponse>{
    return this.http.post<CreateProductResponse>(`${environment.apiUrl}/products`, productData);
  }
}
