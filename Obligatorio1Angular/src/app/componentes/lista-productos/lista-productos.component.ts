import { Component, OnInit } from '@angular/core';
import { ProductService } from 'src/app/servicios/product.service';
import { Product } from 'src/app/modelos/Product';
import { DialogService } from 'src/app/servicios/dialog.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-lista-productos',
  templateUrl: './lista-productos.component.html',
  styleUrls: ['./lista-productos.component.css']
})
export class ListaProductosComponent implements OnInit {
  products: Product[] = [];
  productBeingEdited: Product | null = null;

  constructor(private productService: ProductService, private dialogService: DialogService, private router: Router) { }

  ngOnInit(): void {
    this.getProducts();
  }

  getProducts() {
    this.productService.getProducts().subscribe(
      (products) => {
        this.products = products;
      },
      (error) => {
        console.error('Error al obtener la lista de productos:', error);
        if (error != null && error.error != null && error.error.message != null) {
          this.dialogService.openAlertDialog('Error', error.error.message);
;
        } else {
          this.dialogService.openAlertDialog('Error', error);
        }
      }
    );
  }

  deleteProduct(productID: number) {
    this.productService.deleteProduct(productID).subscribe(
      () => {
        this.products = this.products.filter((product) => product.productID !== productID);
  
        this.dialogService.openAlertDialog('Éxito', 'Producto eliminado correctamente.');
      },
      (error) => {
        console.error('Error al eliminar producto:', error);
        this.dialogService.openAlertDialog('Error', 'Error al eliminar el producto. Intente nuevamente.');
      }
    );
  }
  

  editProduct(product: Product) {
    this.productBeingEdited = { ...product };
  }

  saveProductChanges(updatedProduct: Product) {
    this.productService.updateProduct(updatedProduct).subscribe(
      (response) => {
        this.products = this.products.map((product) =>
          product.productID === updatedProduct.productID ? { ...product, ...response } : product
        );

        this.productBeingEdited = null; 
        this.dialogService.openAlertDialog('Éxito', 'Producto actualizado correctamente.');

        this.productService.getProducts().subscribe(
          (products) => {
            this.products = products;
          },
          (error) => {
            console.error('Error al obtener la lista de productos:', error);
            this.dialogService.openAlertDialog('Error', error.error.message);
          }
        );
      },

      (error) => {
        console.error('Error al actualizar producto:', error);
        this.dialogService.openAlertDialog('Error', 'Error al actulizar el producto. Intente nuevamente.');
      }
    );
  }
}
