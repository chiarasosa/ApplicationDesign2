import { Injectable } from '@angular/core';
import { Product } from '../modelos/Product';
import { Observable, throwError } from 'rxjs';
import { HttpClient, HttpHeaders} from '@angular/common/http';
import { Session } from '../interfaces/session';
import { catchError } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private baseUrl = 'https://localhost:7004/api';
  constructor(private http: HttpClient) { }

  getProducts():Observable<Product[]> {
    return this.http.get<Product[]>(this.baseUrl);
  }

  public registerProduct(product: Product): Observable<void> {
    const token=localStorage.getItem('token');
    if(!token){
      return throwError('Token no disponible.');
    }
    console.log("TOKEN: " + token);
    const url = `${this.baseUrl}/products`;
    const headers = new HttpHeaders({
      Authorization: token,
    });

    return this.http.post<void>(url, product, {headers}).pipe(
      catchError(this.handleError));
  }


  

  private handleError(error: any) {
    console.error('Ocurrió un error:', error);
    return throwError(error);
  }
}
