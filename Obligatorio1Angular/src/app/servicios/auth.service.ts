// Ejemplo de un servicio para gestionar la comunicación entre componentes
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  // BehaviorSubject para rastrear el estado de inicio de sesión
  private isLoggedInSubject = new BehaviorSubject<boolean>(false);

  // Observable que los componentes pueden suscribirse para obtener el estado de inicio de sesión
  isLoggedIn$ = this.isLoggedInSubject.asObservable();

  // Método para cambiar el estado de inicio de sesión y notificar a los observadores
  setLoggedIn(isLoggedIn: boolean) {
    this.isLoggedInSubject.next(isLoggedIn);
  }
}
