import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Product } from '../modelos/Product'; 
import { Cart } from '../modelos/Cart';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  private baseUrl = 'https://localhost:7004/api'; 

  constructor(private http: HttpClient, private router: Router) {}

  

  public addToCart(product: Product): Observable<void> {
    const token = localStorage.getItem('token');
    if (!token) {
      console.error('Token no disponible'); 
      return throwError('Debe iniciar sesión.');
    }

    const url = 'https://localhost:7004/api/carts';
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
      console.error('Token no disponible');
      return throwError('Token no disponible');
    }
  
    const url = `${this.baseUrl}/carts`;
    const headers = new HttpHeaders({
      Authorization: token,
    });
  
    console.log('Enviando solicitud DELETE para eliminar producto:', product); 
  
    return this.http.request<void>('delete', url, {
      headers,
      body: product, 
    }).pipe(
      catchError(this.handleError)
    );
  }
  
  
  public getCart(): Observable<Product[]> {
    const token = localStorage.getItem('token');
    if (!token) {
      console.error('Token no disponible'); 
      this.router.navigate(['/inicioSesion']);
      return throwError('Debe iniciar sesión.');
    }

    const url = `${this.baseUrl}/carts`;
    const headers = new HttpHeaders({
      Authorization: token,
    });

    console.log('Enviando solicitud GET para obtener productos del carrito');

    return this.http.get<Product[]>(url, { headers }).pipe(
      catchError(this.handleError)
    );
  }

  public getPromotionAppliedToCart(): Observable<any> {
    const token = localStorage.getItem('token');
    if (!token) {
      console.error('Debe iniciar sesión.'); 
      return throwError('Debe iniciar sesión.');
    }

    const url = `${this.baseUrl}/carts/PromotionApplied`;
    const headers = new HttpHeaders({
      Authorization: token,
    });

    console.log('Enviando solicitud GET para obtener la promoción aplicada al carrito');

    return this.http.get<any>(url, { headers }).pipe(
      catchError(this.handleError)
    );
  }

  public getTotalPrice(): Observable<number> {
    const token = localStorage.getItem('token');
    if (!token) {
      console.error('Token no disponible'); 
      return throwError('Debe iniciar sesión.');
    }

    const url = `${this.baseUrl}/get-total-price`;
    const headers = new HttpHeaders({
      Authorization: token,
    });

    console.log('Enviando solicitud GET para obtener el precio total del carrito');

    return this.http.get<number>(url, { headers }).pipe(
      catchError(this.handleError)
    );
  }

  private handleError(error: any) {
    console.error('Ocurrió un error:', error);
    return throwError(error);
  }
}
