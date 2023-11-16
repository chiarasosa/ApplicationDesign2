import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { LocalStorageService } from 'src/app/servicios/localStorage';
import { UserService } from 'src/app/servicios/user.service';
import { User } from 'src/app/modelos/User'; 
import { map } from 'rxjs/operators'; 

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(
    private localStorageService: LocalStorageService,
    private userService: UserService,
    private router: Router
  ) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): boolean | UrlTree | Observable<boolean | UrlTree> {
    if (this.localStorageService.isLogged()) {

      return this.userService.getUserFromToken().pipe(
        map((user: User) => {
          if (user && user.role === 'Administrador') {

            return true;
          } else {
            return this.router.createUrlTree(['/inicio-sesion']);
          }
        })
      );
    }

    return this.router.createUrlTree(['/inicio-sesion']);
  }
}