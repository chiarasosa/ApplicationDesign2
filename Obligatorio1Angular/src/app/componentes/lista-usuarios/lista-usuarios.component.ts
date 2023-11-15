import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/servicios/user.service';
import { User } from 'src/app/modelos/User';
import { DialogService } from 'src/app/servicios/dialog.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-lista-usuarios',
  templateUrl: './lista-usuarios.component.html',
  styleUrls: ['./lista-usuarios.component.css']
})
export class ListaUsuariosComponent implements OnInit {
  users: User[] = [];
  userBeingEdited: User | null = null;

  constructor(private userService: UserService, private dialogService: DialogService, private router: Router) {}

  ngOnInit(): void {
    this.userService.getUsuarios().subscribe(
      (users) => {
        this.users = users;
      },
      (error) => {
        console.error('Error al obtener la lista de usuarios:', error);
        if (error!= null && error.error != null && error.errror.message != null)
          this.dialogService.openAlertDialog('Error', error.error.message);
        else
        this.dialogService.openAlertDialog('Error', error);
        this.router.navigate(['/inicioSesion']);
      }
    );
  }

  deleteUser(userID: number) {
    this.userService.deleteUser(userID).subscribe(
      () => {
        // Elimina el usuario de la lista local
        this.users = this.users.filter((user) => user.userID !== userID);
  
        this.dialogService.openAlertDialog('Éxito', 'Usuario eliminado con éxito.');
      },
      (error) => {
        console.error('Error al eliminar usuario:', error);
        this.dialogService.openAlertDialog('Error', 'Error al eliminar usuario. Intente nuevamente.');
      }
    );
  }
  

  editUser(user: User) {
    this.userBeingEdited = { ...user };
  }

  saveUserChanges(updatedUser: User) {
    this.userService.updateUser(updatedUser).subscribe(
      (response) => {
        // Actualiza la lista de usuarios con los datos actualizados
        this.users = this.users.map((user) =>
          user.userID === updatedUser.userID ? { ...user, ...response } : user
        );

        this.userBeingEdited = null; // Sale del modo de edición
        this.dialogService.openAlertDialog('Éxito', 'Usuario actualizado correctamente');
      },
      (error) => {
        console.error('Error al actualizar usuario:', error);
        this.dialogService.openAlertDialog('Error','Error al actualizar usuario. Intente nuevamente.');
      }
    );
  }
}