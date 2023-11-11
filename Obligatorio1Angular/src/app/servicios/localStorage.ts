import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class LocalStorageService {
  // Método para almacenar el token en el localStorage
  setToken(token: string) {
    localStorage.setItem('token', token);
  }

  // Método para obtener el token del localStorage
  getToken(): string | null {
    return localStorage.getItem('token');
  }

  // Método para eliminar el token del localStorage
  removeToken() {
    localStorage.removeItem('token');
  }

    // Método para verificar si un usuario está logeado
    isLogged(): boolean {
        return this.getToken() !== null;
      }
}
