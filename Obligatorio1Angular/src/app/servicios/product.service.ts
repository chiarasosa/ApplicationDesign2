import { Injectable } from '@angular/core';
import { Product } from '../modelos/Product';
import { Observable } from 'rxjs';
import { HttpClient,HttpHeaders, HttpParams } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private baseUrl = 'https://localhost:7004/api';
  constructor(private http: HttpClient) { }

  getProducts():Observable<Product[]> {
    return this.http.get<Product[]>(this.baseUrl);
  }

  public registerProduct(product: Product, userToken:string): Observable<any> {
    const headers = new HttpHeaders()
      .set('userToken', userToken);
    const url = `${this.baseUrl}/products`;
    return this.http.post<void>(url, product);
  }
}
