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
  product: Product = new Product(0, '', 1, '', '', 1, '', 1);

  constructor(private producrService: ProductService, private router: Router, private dialogService: DialogService, private localStorageService: LocalStorageService) { }

  onSubmit(): void {
    if (!this.product.name || !this.product.price || !this.product.description || !this.product.brand || !this.product.category || !this.product.color || !this.product.stock) {
      this.dialogService.openAlertDialog('Error', 'Por favor, completa todos los campos del formulario.');
      return;
    }

    this.producrService.registerProduct(this.product).subscribe(
      (response) => {
        console.log('Producto registrado:', response);

        this.openAlertDialog('Éxito', 'Producto registado con exito.');
        this.product = {
          productID: 0,
          name: '',
          price: 1,
          description: '',
          brand: '',
          category: 1,
          color: '',
          stock: 1,
        };


      },
      (error) => {
        console.error('Error al registrar producto:', error);
        if (error != null && error.error != null && error.error.message != null) {
          this.dialogService.openAlertDialog('Error', error.error.message);
        }
        else if (error != null && error.error != null && error.error.title != null) {
          this.dialogService.openAlertDialog('Error', error.error.title);
        }
        else {
          this.dialogService.openAlertDialog('Error', error);
        }
      }
    );
  }

  openAlertDialog(title: string, message: string) {
    this.dialogService.openAlertDialog(title, message);
  }
}