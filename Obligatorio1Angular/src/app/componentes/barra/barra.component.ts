import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LocalStorageService } from 'src/app/servicios/localStorage';
import { AlertDialogComponent } from '../alert-dialog/alert-dialog.component';
import { DialogService } from 'src/app/servicios/dialog.service';
import { CarritoComponent } from 'src/app/componentes/carrito/carrito.component';

@Component({
  selector: 'app-barra',
  templateUrl: './barra.component.html',
  styleUrls: ['./barra.component.css']
})
export class BarraComponent {
  userRole: string = '';
  activeRoute: string = '';
  isLoggedIn: boolean = false; // Variable para controlar si el usuario ha iniciado sesión

  constructor(private router: Router, private localStorageService: LocalStorageService, private dialogService: DialogService) {
    // Verifica si hay un token en el LocalStorage para determinar si el usuario ha iniciado sesión
    this.isLoggedIn = !!this.localStorageService.getToken();

    const userData = localStorage.getItem('user');
    if (userData) {
      const user = JSON.parse(userData);
      if (user && user.role) {
        this.userRole = user.role;
      }
    }
  }

  logout() {
    this.localStorageService.removeToken();
    this.isLoggedIn = false; // Cuando cierras sesión, establece isLoggedIn en falso
    this.openAlertDialog('Éxito', 'Se cerró la sesión');
  }

  setActiveRoute(route: string): void {
    this.activeRoute = route;
  }

  openAlertDialog(title: string, message: string) {
    this.dialogService.openAlertDialog(title, message);
  }
}
