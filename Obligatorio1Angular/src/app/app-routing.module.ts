import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RegistroComponent } from './componentes/registrar/registro.component';
import { InicioSesionComponent } from './componentes/inicio-sesion/inicio-sesion.component';
import { BarraComponent } from './componentes/barra/barra.component';
import { ProductoComponent } from './componentes/producto/producto.component';
import { ListaProductosComponent } from './componentes/lista-productos/lista-productos.component';
import { LandingPageComponent } from './componentes/landing-page/landing-page.component';
import { ListaUsuariosComponent } from './componentes/lista-usuarios/lista-usuarios.component'; // Importa el componente ListaUsuariosComponent

const routes: Routes = [
  { path: 'inicioSesion', component: InicioSesionComponent },
  { path: 'Registrar', component: RegistroComponent },
  { path: 'barra', component: BarraComponent },
  { path: 'producto', component: ProductoComponent },
  { path: 'lista-productos', component: ListaProductosComponent },
  { path: 'landing', component: LandingPageComponent },
  { path: 'lista-usuarios', component: ListaUsuariosComponent }, // Agrega esta línea
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
