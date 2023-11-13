import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RegistroComponent } from './componentes/registrar/registro.component';
import { InicioSesionComponent } from './componentes/inicio-sesion/inicio-sesion.component';
import { BarraComponent } from './componentes/barra/barra.component';
import { ProductoComponent } from './componentes/producto/producto.component';
import { ListaProductosComponent } from './componentes/lista-productos/lista-productos.component';
import { LandingPageComponent } from './componentes/landing-page/landing-page.component';
import { ListaUsuariosComponent } from './componentes/lista-usuarios/lista-usuarios.component'; // Importa el componente ListaUsuariosComponent
import { CrearProductoComponent } from './componentes/crear-producto/crear-producto.component';
import { ListaProductosCompraComponent } from './componentes/lista-productos-compra/lista-productos-compra.component';
import { CompraComponent } from './componentes/compra/compra.component';
import { CarritoComponent } from './componentes/carrito/carrito.component';

const routes: Routes = [
  { path: 'inicioSesion', component: InicioSesionComponent },
  { path: 'Registrar', component: RegistroComponent },
  { path: 'barra', component: BarraComponent },
  { path: 'producto', component: ProductoComponent },
  { path: 'lista-productos', component: ListaProductosComponent },
  { path: 'landing', component: LandingPageComponent },
  { path: 'lista-usuarios', component: ListaUsuariosComponent }, // Agrega esta línea
  { path:'crear-producto',component:CrearProductoComponent},
  { path:'Compra', component: CompraComponent},
  { path:'lista-productos-compra', component: ListaProductosCompraComponent},
  { path:'carrito', component: CarritoComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
