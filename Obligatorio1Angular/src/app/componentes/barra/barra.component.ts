import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LocalStorageService } from 'src/app/servicios/localStorage';
import { AlertDialogComponent } from '../alert-dialog/alert-dialog.component';
import { DialogService } from 'src/app/servicios/dialog.service';
import { CarritoComponent } from 'src/app/componentes/carrito/carrito.component';
import { AuthService } from 'src/app/servicios/auth.service';

@Component({
  selector: 'app-barra',
  templateUrl: './barra.component.html',
  styleUrls: ['./barra.component.css']
})
export class BarraComponent {
  userRole: string = '';
  activeRoute: string = '';
  isLoggedIn: boolean = false;

  constructor(
    private router: Router,
    private localStorageService: LocalStorageService,
    private dialogService: DialogService,
    private authService: AuthService 
  ) {

    this.isLoggedIn = !!this.localStorageService.getToken();
    this.authService.isLoggedIn$.subscribe((isLoggedIn) => {
      this.isLoggedIn = isLoggedIn;
    });
      

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
    this.authService.setLoggedIn(false); 
    this.openAlertDialog('Éxito', 'Se cerró la sesión');
  }

  setActiveRoute(route: string): void {
    this.activeRoute = route;
  }

  openAlertDialog(title: string, message: string) {
    this.dialogService.openAlertDialog(title, message);
  }
}
