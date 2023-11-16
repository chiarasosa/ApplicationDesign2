import { Component } from '@angular/core';
import { UserService } from 'src/app/servicios/user.service';
import { DialogService } from 'src/app/servicios/dialog.service';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-registro',
  templateUrl: './registro.component.html',
  styleUrls: ['./registro.component.css'],
})
export class RegistroComponent {
  user: any = { rol: '' };

  constructor(private userService: UserService, private dialogService: DialogService) {}

  registerUser() {
    if (!this.user.username || !this.user.password || !this.user.email || !this.user.address || !this.user.role) {
      this.openAlertDialog('Error', 'Por favor, completa todos los campos del formulario.');
      return;
    }

    this.userService.registerUser(this.user).subscribe(
      (response) => {
        console.log('Usuario registrado:', response);
        this.openAlertDialog('Éxito', 'Usuario registrado con éxito.');
        this.user = {
          UserID: 0,
          UserName: '',
          Password: '',
          Email: '',
          Address: '',
          Role: '',
        };
      },
      (error) => {
        console.error('Error al registrar el usuario:', error);
        this.openAlertDialog('Error', 'Error al registrar el usuario. Intente nuevamente.');
      }
    );
  }

  openAlertDialog(title: string, message: string) {
    this.dialogService.openAlertDialog(title, message);
  }
}
