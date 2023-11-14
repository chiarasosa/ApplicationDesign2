import { Component, OnInit } from '@angular/core';
import { PurchaseService } from 'src/app/servicios/purchase.service';
import { Purchase } from 'src/app/modelos/Purchase';
import { DialogService } from 'src/app/servicios/dialog.service';
import { User } from 'src/app/modelos/User';

@Component({
  selector: 'app-lista-compras',
  templateUrl: './lista-compras.component.html',
  styleUrls: ['./lista-compras.component.css']
})
export class ListaComprasComponent implements OnInit {
  compras: Purchase[] = [];

  constructor(private purchaseService: PurchaseService, private dialogService: DialogService) { }

  ngOnInit(): void {
    this.obtenerCompras();
  }

  obtenerCompras(): void {
    this.purchaseService.getCompras().subscribe(
      (compras) => {
        this.compras = compras;
        console.log('Compras recibidas:', this.compras);
      },
      (error) => {
        console.error('Error al obtener la lista de compras:', error);
        // Puedes manejar el error según tus necesidades, por ejemplo, mostrar un mensaje al usuario
        // this.dialogService.openAlertDialog('Error', 'Error al obtener la lista de compras. Intente nuevamente.');
      }
    );
  }
}
