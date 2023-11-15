import { Component, OnInit } from '@angular/core';
import { PurchaseService } from 'src/app/servicios/purchase.service';
import { Purchase } from 'src/app/modelos/Purchase';
import { DialogService } from 'src/app/servicios/dialog.service';
import { User } from 'src/app/modelos/User';
import { Router } from '@angular/router';


@Component({
  selector: 'app-lista-compras',
  templateUrl: './lista-compras.component.html',
  styleUrls: ['./lista-compras.component.css']
})
export class ListaComprasComponent implements OnInit {
  compras: Purchase[] = [];

  constructor(private purchaseService: PurchaseService, private dialogService: DialogService, private router: Router) { }

  ngOnInit(): void {
    this.obtenerCompras();
  }

obtenerCompras(): void {
  this.purchaseService.getCompras().subscribe(
    (response) => {
      if ('message' in response && response.message === 'No se encontraron compras en el sistema.') {
        // Mostrar mensaje cuando no hay compras
        this.dialogService.openAlertDialog('Información', 'No hay compras registradas para este usuario.');
      } else {
        // Se recibieron compras
        const compras = response as Purchase[];
        if (compras && compras.length > 0) {
          this.compras = compras;
          console.log('Compras recibidas:', this.compras);
        }
      }
    },
    (error) => {
      console.error('Error al obtener la lista de compras:', error);

      let errorMessage = 'Error al obtener la lista de compras.';

      if (error && error.error && error.error.message) {
        errorMessage = error.error.message;
      }

      this.dialogService.openAlertDialog('Error', errorMessage);

      this.dialogService.okClicked$.subscribe(() => {
        this.router.navigate(['/inicioSesion']);
      });
    }
  );
}

  
}
