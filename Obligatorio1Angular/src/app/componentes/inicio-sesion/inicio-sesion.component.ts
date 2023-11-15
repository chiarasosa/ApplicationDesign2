import { Component } from '@angular/core';
import { UserService } from 'src/app/servicios/user.service';
import { Router } from '@angular/router';
import { User } from 'src/app/modelos/User';
import { LocalStorageService } from 'src/app/servicios/localStorage';
import { DialogService } from 'src/app/servicios/dialog.service';
import { AuthService } from 'src/app/servicios/auth.service';

@Component({
  selector: 'app-inicio-sesion',
  templateUrl: './inicio-sesion.component.html',
  styleUrls: ['./inicio-sesion.component.css'],
})
export class InicioSesionComponent {
  user: User = new User(0, '', '', '', '', '');
  isLoggedIn: boolean = false; // Variable para controlar el estado del inicio de sesión
  constructor(
    private userService: UserService,
    private router: Router,
    private dialogService: DialogService,
    private localStorageService: LocalStorageService,
    private authService: AuthService
  ) {}
  
  loginUser() {
    this.userService.loginUser(this.user).subscribe(
      (response) => {
        console.log('Inicio de sesión exitoso:', response);
        this.localStorageService.setToken(response.token);
        this.authService.setLoggedIn(true); // Notifica a otros componentes que el usuario ha iniciado sesión
        this.isLoggedIn= true;
        this.openAlertDialog('Éxito', 'Inicio de sesión exitoso');
  
        // No es necesario recargar la página
        // Redirige a la página deseada
        // this.router.navigate(['/pagina-deseada']);
      },
      (error) => {
        console.error('Error en el inicio de sesión:', error);
        this.openAlertDialog('Error', 'Error al iniciar sesión. Intente nuevamente.');
      }
    );
  }
  

  openAlertDialog(title: string, message: string) {
    this.dialogService.openAlertDialog(title, message);
  }
}
