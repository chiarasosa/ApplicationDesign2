// userService.ts

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../modelos/User';
import { IUserService } from '../interfaces/user-service';
import {Session } from '../interfaces/session';

@Injectable({
  providedIn: 'root',
})
export class UserService implements IUserService {
  private baseUrl = 'https://localhost:7004/api';

  constructor(private http: HttpClient) {}

  public registerUser(user: User): Observable<void> {
    const url = `${this.baseUrl}/users`;
    return this.http.post<void>(url, user);
  }

 // Método para el inicio de sesión
   // Ajusta la firma del método para coincidir con la interfaz
   public loginUser(user: User): Observable<Session> {
    const url = `${this.baseUrl}/sessions`;
    return this.http.post<Session>(url, user);
  }
}
