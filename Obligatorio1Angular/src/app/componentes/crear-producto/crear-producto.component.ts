import { Component } from '@angular/core';
import { Product } from 'src/app/modelos/Product';
import { DialogService } from 'src/app/servicios/dialog.service';
import { LocalStorageService } from 'src/app/servicios/localStorage';
import { ProductService } from 'src/app/servicios/product.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-crear-producto',
  templateUrl: './crear-producto.component.html',
  styleUrls: ['./crear-producto.component.css']
})
export class CrearProductoComponent {
  product: Product = new Product(0, '', 0, '', 0, 0, '', 0);

  constructor(private producrService: ProductService, private router: Router, private dialogService: DialogService, private localStorageService: LocalStorageService) { }

  onSubmit(): void {
    this.producrService.registerProduct(this.product).subscribe(
      (response) => {
        // Maneja la respuesta del inicio de sesión aquí
        console.log('Producto registrado:', response);

        this.openAlertDialog('Éxito', 'Producto registado con exito.');
            // Restablece los campos del formulario después del registro
      this.product = {
        productID: 0,
        name: '',
        price: 0,
        description: '',
      brand: 0  ,
        category: 0,
        color: '',
        stock: 0,
      };


      },
      (error) => {
        console.error('Error al registrar producto:', error);
        if (error != null && error.error != null && error.error.message != null) {
          this.dialogService.openAlertDialog('Error', error.error.message);

        } else {
          this.dialogService.openAlertDialog('Error', error);

        }
      }
    );
  }

  openAlertDialog(title: string, message: string) {
    this.dialogService.openAlertDialog(title, message); // Usa dialogService
  }
}