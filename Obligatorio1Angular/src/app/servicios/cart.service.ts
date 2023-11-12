import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Product } from '../modelos/Product'; // Asegúrate de importar el modelo Product

@Injectable({
  providedIn: 'root',
})
export class CartService {
  private baseUrl = 'https://localhost:7004/api/carts'; // Asegúrate de que la URL sea correcta

  constructor(private http: HttpClient) {}

  public addToCart(product: Product): Observable<void> {
    const token = localStorage.getItem('token');
    if (!token) {
      return throwError('Token no disponible');
    }

    const url = `${this.baseUrl}/add-product`;
    const headers = new HttpHeaders({
      Authorization: token,
    });

    return this.http.post<void>(url, product, { headers }).pipe(
      catchError(this.handleError)
    );
  }

  public removeFromCart(product: Product): Observable<void> {
    const token = localStorage.getItem('token');
    if (!token) {
      return throwError('Token no disponible');
    }

    const url = `${this.baseUrl}/remove-product`;
    const headers = new HttpHeaders({
      Authorization: token,
    });

    return this.http.post<void>(url, product, { headers }).pipe(
      catchError(this.handleError)
    );
  }

  public getCart(): Observable<Product[]> {
    const token = localStorage.getItem('token');
    if (!token) {
      return throwError('Token no disponible');
    }

    const url = `${this.baseUrl}/get-products`;
    const headers = new HttpHeaders({
      Authorization: token,
    });

    return this.http.get<Product[]>(url, { headers }).pipe(
      catchError(this.handleError)
    );
  }

  public getPromotionAppliedToCart(): Observable<any> {
    const token = localStorage.getItem('token');
    if (!token) {
      return throwError('Token no disponible');
    }

    const url = `${this.baseUrl}/get-promotion`;
    const headers = new HttpHeaders({
      Authorization: token,
    });

    return this.http.get<any>(url, { headers }).pipe(
      catchError(this.handleError)
    );
  }

  public getTotalPrice(): Observable<number> {
    const token = localStorage.getItem('token');
    if (!token) {
      return throwError('Token no disponible');
    }

    const url = `${this.baseUrl}/get-total-price`;
    const headers = new HttpHeaders({
      Authorization: token,
    });

    return this.http.get<number>(url, { headers }).pipe(
      catchError(this.handleError)
    );
  }

  private handleError(error: any) {
    console.error('Ocurrió un error:', error);
    return throwError(error);
  }
}
