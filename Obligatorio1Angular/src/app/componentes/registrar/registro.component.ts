import { Component } from '@angular/core';
import { UserService } from 'src/app/servicios/user.service';
import { DialogService } from 'src/app/servicios/dialog.service'; // Asegúrate de importar el servicio DialogService
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-registro',
  templateUrl: './registro.component.html',
  styleUrls: ['./registro.component.css'],
})
export class RegistroComponent {
  user: any = { rol: '' };

  constructor(private userService: UserService, private dialogService: DialogService) {} // Usa dialogService

  registerUser() {
    this.userService.registerUser(this.user).subscribe(
      (response) => {
        console.log('Usuario registrado con éxito:', response);
        this.openAlertDialog('Éxito', 'Usuario registrado con éxito');
      },
      (error) => {
        console.error('Error al registrar usuario:', error);
        this.openAlertDialog('Error', 'No se pudo registrar el usuario');
      }
    );
  }

  // No olvides definir el método openAlertDialog como se mencionó en los pasos anteriores
  openAlertDialog(title: string, message: string) {
    this.dialogService.openAlertDialog(title, message); // Usa dialogService
  }
}
