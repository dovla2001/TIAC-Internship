import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { CreateVariantRequest } from '../models/productVariant.models';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductVariant {
  private apiUrl = `${environment.apiUrl}/productVariant`;

  constructor(private http: HttpClient){}

  public createVariant(variantData: CreateVariantRequest): Observable<any> {
    return this.http.post(this.apiUrl, variantData);
  }
}
