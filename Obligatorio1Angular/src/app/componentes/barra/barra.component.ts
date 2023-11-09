import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-barra',
  templateUrl: './barra.component.html',
  styleUrls: ['./barra.component.css']
})
export class BarraComponent {
  userRole: string = '';
  activeRoute: string = '';

  constructor(private router:Router){

    const userData = localStorage.getItem('user');
    if (userData) {
      const user = JSON.parse(userData);
      if (user && user.role) {
        this.userRole = user.role;
      }
    }
  }

  setActiveRoute(route: string): void {
    this.activeRoute = route;
  }
}
