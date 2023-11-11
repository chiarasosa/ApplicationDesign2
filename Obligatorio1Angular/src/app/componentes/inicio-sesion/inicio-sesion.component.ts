// inicio-sesion.component.ts
import { MatDialog } from '@angular/material/dialog';
import { Component } from '@angular/core';
import { UserService } from 'src/app/servicios/user.service';
import { Router } from '@angular/router';
import { User } from 'src/app/modelos/User'; // Asegúrate de importar el modelo User adecuado
import { DialogService } from 'src/app/servicios/dialog.service'; // Asegúrate de importar el servicio DialogService
import { LocalStorageService } from 'src/app/servicios/localStorage';

@Component({
  selector: 'app-inicio-sesion',
  templateUrl: './inicio-sesion.component.html',
  styleUrls: ['./inicio-sesion.component.css'],
})
export class InicioSesionComponent {
  user: User = new User('', '', '', '', ''); // Crea una nueva instancia de User

  constructor(private userService: UserService, private router: Router, private dialogService: DialogService, private localStorageService: LocalStorageService) {}

  loginUser() {
    this.userService.loginUser(this.user).subscribe(
      (response) => {
        // Maneja la respuesta del inicio de sesión aquí
        console.log('Inicio de sesión exitoso:', response);
        this.localStorageService.setToken(response.token);
        this.openAlertDialog('Éxito', 'Inicio de sesión exitoso');

        // Redirige a la página deseada
       // this.router.navigate(['/pagina-deseada']); // Reemplaza 'pagina-deseada' con la ruta a la página que deseas después del inicio de sesión
      },
      (error) => {
        console.error('Error en el inicio de sesión:', error);
        this.openAlertDialog('Error', 'Error en el inicio de sesión');

        // Maneja errores de inicio de sesión aquí
      }
    );
  }
  
  // No olvides definir el método openAlertDialog como se mencionó en los pasos anteriores
  openAlertDialog(title: string, message: string) {
    this.dialogService.openAlertDialog(title, message); // Usa dialogService
  }
  
}
