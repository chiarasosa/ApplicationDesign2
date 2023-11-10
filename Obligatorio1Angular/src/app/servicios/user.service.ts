import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../interfaces/user.model'; // Asegúrate de importar la interfaz User desde donde corresponda

@Injectable({
  providedIn: 'root',
})
export class UserService {

  private baseUrl = 'https://localhost:7004/api'; // Reemplaza con la URL de tu API .NET

  constructor(private http: HttpClient) { }

  public registerUser(user: User): Observable<any> {
    const url = `${this.baseUrl}/users`; // Asegúrate de que la ruta coincida con la de tu API .NET
    return this.http.post(url, user);
  }
}
