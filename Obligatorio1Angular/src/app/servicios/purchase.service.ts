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

  public registerPurchase(purchase: Purchase): Observable<void> {
    const url = `${this.baseUrl}/purchases`;
    return this.http.post<void>(url, purchase).pipe(
      catchError(this.handleError)
    );
  }


  private handleError(error: any) {
    console.error('Ocurrió un error:', error);
    return throwError(error);
  }
}
