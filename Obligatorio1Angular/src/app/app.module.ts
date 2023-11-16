import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button'; 
import { DialogService } from './servicios/dialog.service';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { InicioSesionComponent } from './componentes/inicio-sesion/inicio-sesion.component';
import { RegistroComponent } from './componentes/registrar/registro.component';
import { LandingPageComponent } from './componentes/landing-page/landing-page.component';
import { BarraComponent } from './componentes/barra/barra.component';
import { ListaProductosComponent } from './componentes/lista-productos/lista-productos.component';
import { ProductoComponent } from './componentes/producto/producto.component';
//import { PromocionComponent } from './componentes/promocion/promocion.component';
import { CompraComponent } from './componentes/compra/compra.component';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { AlertDialogComponent } from './componentes/alert-dialog/alert-dialog.component';
import { CrearProductoComponent } from './componentes/crear-producto/crear-producto.component';
import { ListaUsuariosComponent } from './componentes/lista-usuarios/lista-usuarios.component';
import { ListaProductosCompraComponent } from './componentes/lista-productos-compra/lista-productos-compra.component';
import { CarritoComponent } from './componentes/carrito/carrito.component';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatTableModule } from '@angular/material/table';
import { ListaComprasComponent } from './componentes/lista-compras/lista-compras.component';

@NgModule({
  declarations: [
    AppComponent,
    InicioSesionComponent,
    RegistroComponent,
    LandingPageComponent,
    BarraComponent,
    ListaProductosComponent,
    ProductoComponent,
    //PromocionComponent,
    CompraComponent,
    AlertDialogComponent,
    CrearProductoComponent,
    ListaUsuariosComponent,
    ListaProductosCompraComponent,
    CarritoComponent,
    ListaComprasComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    MatDialogModule,
    MatButtonModule,
    NoopAnimationsModule, 
    MatPaginatorModule, 
    MatTableModule
  ],
  providers: [DialogService],
  bootstrap: [AppComponent],
})
export class AppModule {}
