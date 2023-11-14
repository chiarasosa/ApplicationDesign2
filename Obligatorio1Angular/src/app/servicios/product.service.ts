import { Injectable } from '@angular/core';
import { Product } from '../modelos/Product';
import { Observable, throwError } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private baseUrl = 'https://localhost:7004/api/products';

  constructor(private http: HttpClient) {}

  getProducts(): Observable<Product[]> {
    const token = localStorage.getItem('token');
    if (!token) {
      return throwError('Token no disponible');
    }

    const headers = new HttpHeaders({
      Authorization: token,
    });

    return this.http.get<Product[]>(this.baseUrl, { headers }).pipe(
      catchError(this.handleError)
    );
  }

  public registerProduct(product: Product): Observable<void> {
    const token = localStorage.getItem('token');
    if (!token) {
      return throwError('Token no disponible');
    }

    const url = `${this.baseUrl}`;
    const headers = new HttpHeaders({
      Authorization: token,
    });

    return this.http.post<void>(url, product, { headers }).pipe(
      catchError(this.handleError)
    );
  }

  public deleteProduct(productID: number): Observable<void> {
    const token = localStorage.getItem('token');
    if (!token) {
      return throwError('Token no disponible');
    }

    const url = `${this.baseUrl}/${productID}`;
    const headers = new HttpHeaders({
      Authorization: token,
    });

    return this.http
      .delete<void>(url, { headers })
      .pipe(catchError(this.handleError));
  }

  public updateProduct(updatedProduct: Product): Observable<Product> {
    const token = localStorage.getItem('token');
    if (!token) {
      return throwError('Token no disponible');
    }

    const url = `${this.baseUrl}/${updatedProduct.productID}`;
    const headers = new HttpHeaders({
      Authorization: token,
    });

    return this.http
      .put<Product>(url, updatedProduct, { headers })
      .pipe(catchError(this.handleError));
  }

  getProductss(): Observable<Product[]> {
    const token = localStorage.getItem('token');
    if (!token) {
      return throwError('Token no disponible');
    }

    const url = `${this.baseUrl}/GetProducts`;
    const headers = new HttpHeaders({
      Authorization: token,
    });

    return this.http.get<Product[]>(url).pipe(
      catchError(this.handleError)
    );
  }

  private handleError(error: any) {
    console.error('Ocurrió un error:', error);
    return throwError(error);
  }
}
