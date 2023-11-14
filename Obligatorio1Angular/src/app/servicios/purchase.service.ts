import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Purchase } from '../modelos/Purchase';
import { Cart } from '../modelos/Cart';
import { DialogService } from 'src/app/servicios/dialog.service';

@Injectable({
  providedIn: 'root'
})
export class PurchaseService {
  private baseUrl = 'https://localhost:7004/api';
  constructor(private http: HttpClient) { }

  public registerPurchase(purchase: Purchase, selectedPaymentMethod: string): Observable<void> {
    const token = localStorage.getItem('token');
    if (!token) {
      return throwError('Token no disponible');
    }
 
    const headers = new HttpHeaders({
      Authorization: token,
    });

    const url = `${this.baseUrl}/purchases`;
    purchase.paymentMethod = selectedPaymentMethod;
    console.log(purchase.paymentMethod);
    return this.http.post<void>(url, purchase,{headers}).pipe(
      catchError(this.handleError)
    );
  }


  private handleError(error: any) {
    console.error('Ocurrió un error:', error);
    return throwError(error);
  }
}
