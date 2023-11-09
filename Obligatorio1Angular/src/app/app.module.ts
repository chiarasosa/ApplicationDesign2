import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { InicioSesionComponent } from './componentes/inicio-sesion/inicio-sesion.component';
import { RegistrarComponent } from './componentes/registrar/registrar.component';
import { LandingPageComponent } from './componentes/landing-page/landing-page.component';
import { BarraComponent } from './componentes/barra/barra.component';
import { ListaProductosComponent } from './componentes/lista-productos/lista-productos.component';
import { ProductoComponent } from './componentes/producto/producto.component';
import { PromocionComponent } from './promocion/promocion.component';
import { CompraComponent } from './compra/compra.component';

@NgModule({
  declarations: [
    AppComponent,
    InicioSesionComponent,
    RegistrarComponent,
    LandingPageComponent,
    BarraComponent,
    ListaProductosComponent,
    ProductoComponent,
    PromocionComponent,
    CompraComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
