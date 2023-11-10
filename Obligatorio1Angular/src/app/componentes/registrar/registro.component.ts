import { Component } from '@angular/core';
import { UserService } from 'src/app/servicios/user.service';

@Component({
  selector: 'app-registro',
  templateUrl: './registro.component.html',
  styleUrls: ['./registro.component.css'],
})
export class RegistroComponent {
  user: any = { rol: '' }; // Asegúrate de inicializar la propiedad rol con un valor adecuado si es requerido


  constructor(private userService: UserService) {}

  registerUser() {
    this.userService.registerUser(this.user).subscribe(
      (response) => {
        console.log('Usuario registrado con éxito:', response);
        // Realiza acciones adicionales si es necesario
      },
      (error) => {
        console.error('Error al registrar usuario:', error);
        // Muestra un mensaje de error al usuario si es necesario
      }
    );
  }
}