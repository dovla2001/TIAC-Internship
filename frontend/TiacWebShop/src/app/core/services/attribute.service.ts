import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AttributeResponse, AttributeWithValues, CreateAttributeRequest, ValueData } from '../models/attributes.models';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})

export class AttributeService {

  constructor(private http: HttpClient) { }

  public createAttribute(attribute: CreateAttributeRequest): Observable<AttributeResponse> {
    return this.http.post<AttributeResponse>(`${environment.apiUrl}/attributes`, attribute);
  }

  public getAttributes(): Observable<AttributeResponse[]> {
    return this.http.get<AttributeResponse[]>(`${environment.apiUrl}/attributes/getAll`);
  }

  public addAttributeValue(attributeId: number, value: ValueData): Observable<AttributeResponse> {
    return this.http.post<AttributeResponse>(`${environment.apiUrl}/attributes/${attributeId}/values`, value);
  }

  public getAttributesWithValues(): Observable<AttributeWithValues[]> {
    return this.http.get<AttributeWithValues[]>(`${environment.apiUrl}/attributes/with-values`);
  }
}