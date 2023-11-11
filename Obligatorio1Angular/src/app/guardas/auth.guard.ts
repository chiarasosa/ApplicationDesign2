/*
import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { LocalStorageService } from 'src/app/servicios/localStorage';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(private localStorageService: LocalStorageService, private router: Router) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): boolean | UrlTree | Observable<boolean | UrlTree> {
    if (this.localStorageService.isLogged()) {
      // El usuario está autenticado
      // Verifica los roles (aquí puedes adaptar la lógica según tus necesidades)
      const requiredRoles = route.data.role as string[];
      if (requiredRoles && requiredRoles.some(role => this.localStorageService.hasRole(role))) {
        return true; // El usuario tiene al menos uno de los roles requeridos
      }
    }

    // Redirige al inicio de sesión si el usuario no cumple con los requisitos
    return this.router.createUrlTree(['/inicio-sesion']);
  }
}
*/