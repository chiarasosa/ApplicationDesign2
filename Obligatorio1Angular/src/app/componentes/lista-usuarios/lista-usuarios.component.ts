import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/servicios/user.service'; // Asegúrate de importar el servicio adecuado
import { User } from 'src/app/modelos/User'; // Asegúrate de importar la clase User o el modelo de usuario adecuado

@Component({
  selector: 'app-lista-usuarios',
  templateUrl: './lista-usuarios.component.html',
  styleUrls: ['./lista-usuarios.component.css']
})
export class ListaUsuariosComponent implements OnInit {
  users: User[] = [];

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.userService.getUsuarios().subscribe(users => {
      this.users = users;
    });
  }
}
