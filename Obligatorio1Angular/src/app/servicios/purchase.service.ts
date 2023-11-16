import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError, of, forkJoin } from 'rxjs'; 
import { catchError, mergeMap } from 'rxjs/operators';
import { Purchase } from '../modelos/Purchase';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class PurchaseService {
  private baseUrl = 'https://localhost:7004/api';

  constructor(private http: HttpClient, private router: Router) { }

  public registerPurchase(purchase: Purchase, selectedPaymentMethod: string): Observable<void> {
    const token = localStorage.getItem('token');
    if (!token) {
      return throwError('Debe iniciar sesión.');
    }

    const headers = new HttpHeaders({
      Authorization: token,
    });

    const url = `${this.baseUrl}/purchases`;
    purchase.paymentMethod = selectedPaymentMethod;
    return this.http.post<void>(url, purchase, { headers }).pipe(
      catchError(this.handleError)
    );
  }

  public getCompras(): Observable<Purchase[]> {
    const token = localStorage.getItem('token');
    if (!token) {
      this.router.navigate(['/inicioSesion']);
      return throwError('Debe iniciar sesión.');
    }

    const headers = new HttpHeaders({
      Authorization: token,
    });

    const url = `${this.baseUrl}/purchases`;
    return this.http.get<Purchase[]>(url, { headers }).pipe(
      catchError(this.handleError)
    );
  }

  private getUserById(userId: number): Observable<any> {
    const token = localStorage.getItem('token');
    if (!token) {
      return throwError('Debe iniciar sesión.');
    }

    const headers = new HttpHeaders({
      Authorization: token,
    });
    const userUrl = `${this.baseUrl}/users/${userId}`;
    return this.http.get<any>(userUrl, { headers }).pipe(
      catchError(this.handleError)
    );
  }

  private handleError(error: any) {
    console.error('Ocurrió un error:', error);
    return throwError(error);
  }
}