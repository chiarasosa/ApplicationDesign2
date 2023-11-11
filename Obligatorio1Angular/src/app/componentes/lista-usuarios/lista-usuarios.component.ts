import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/servicios/user.service';
import { User } from 'src/app/modelos/User';
import { DialogService } from 'src/app/servicios/dialog.service';

@Component({
  selector: 'app-lista-usuarios',
  templateUrl: './lista-usuarios.component.html',
  styleUrls: ['./lista-usuarios.component.css']
})
export class ListaUsuariosComponent implements OnInit {
  users: User[] = [];

  constructor(private userService: UserService, private dialogService: DialogService) {}

  ngOnInit(): void {
    this.userService.getUsuarios().subscribe(
      users => {
        this.users = users;
      },
      error => {
        console.error('Error al obtener la lista de usuarios:', error);
        this.dialogService.openAlertDialog('Error', error.message); // Abre la modal y muestra el mensaje de error
      }
    );
  }

  deleteUser(userID: number) {
    this.userService.deleteUser(userID).subscribe(
      () => {
        // Elimina el usuario de la lista actual
        this.users = this.users.filter(user => user.userID !== userID);
      },
      (error) => {
        console.error('Error al eliminar usuario:', error);
        // Muestra un mensaje de error, por ejemplo, usando tu servicio de diálogo
        this.dialogService.openAlertDialog('Error', error.message);
      }
    );
  }
}
