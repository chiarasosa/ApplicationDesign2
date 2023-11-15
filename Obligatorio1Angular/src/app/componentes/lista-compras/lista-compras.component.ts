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
      if (error != null && error.error != null && error.error.message != null) {
        this.dialogService.openAlertDialog('Error', error.error.message);
;
      } else {
        this.dialogService.openAlertDialog('Error', error);
      }

    }
  );
}

  
}
