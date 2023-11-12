import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { User } from '../modelos/User';
import { Session } from '../interfaces/session';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private baseUrl = 'https://localhost:7004/api';

  constructor(private http: HttpClient) {}

  public registerUser(user: User): Observable<void> {
    const url = `${this.baseUrl}/users`;
    return this.http.post<void>(url, user).pipe(
      catchError(this.handleError)
    );
  }

  public loginUser(user: User): Observable<Session> {
    const url = `${this.baseUrl}/sessions`;
    return this.http.post<Session>(url, user).pipe(
      catchError(this.handleError)
    );
  }

  public getUserFromToken(): Observable<User> {
    const token = localStorage.getItem('token');
    if (!token) {
      return throwError('Token no disponible');
    }

    const url = `${this.baseUrl}/current-user`;
    const headers = new HttpHeaders({
      Authorization: token,
    });

    return this.http
      .get<User>(url, { headers })
      .pipe(catchError(this.handleError));
  }

  public getUsuarios(): Observable<User[]> {
    const token = localStorage.getItem('token');
    console.log("TOKEN: " + token);
    if (!token) {
      return throwError('Token no disponible');
    }

    const url = `${this.baseUrl}/users`;
    const headers = new HttpHeaders({
      Authorization: token,
    });

    return this.http
      .get<User[]>(url, { headers })
      .pipe(catchError(this.handleError));
  }

  public deleteUser(userID: number): Observable<void> {
    const token = localStorage.getItem('token');
    if (!token) {
      return throwError('Token no disponible');
    }

    const url = `${this.baseUrl}/users/${userID}`;
    const headers = new HttpHeaders({
      Authorization: token,
    });

    return this.http
      .delete<void>(url, { headers })
      .pipe(catchError(this.handleError));
  }

  public updateUser(updatedUser: User): Observable<User> {
    const token = localStorage.getItem('token');
    if (!token) {
      return throwError('Token no disponible');
    }

    const url = `${this.baseUrl}/users/${updatedUser.userID}`;
    const headers = new HttpHeaders({
      Authorization: token,
    });

    return this.http
      .put<User>(url, updatedUser, { headers })
      .pipe(catchError(this.handleError));
  }

  private handleError(error: any) {
    console.error('Ocurrió un error:', error);
    return throwError(error);
  }
}
