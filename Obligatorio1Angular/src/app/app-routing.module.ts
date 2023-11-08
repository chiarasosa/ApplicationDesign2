import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RegistrarComponent } from './componentes/registrar/registrar.component';
import { InicioSesionComponent } from './componentes/inicio-sesion/inicio-sesion.component';

const routes: Routes = [
  { path: 'inicioSesion', component: InicioSesionComponent },
  {path: 'Registrar', component:RegistrarComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
