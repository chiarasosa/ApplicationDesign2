import { Component, OnInit } from '@angular/core';
import { ProductService } from 'src/app/servicios/product.service';
import { Product } from 'src/app/modelos/Product';
import { CartService } from 'src/app/servicios/cart.service';
import { DialogService } from 'src/app/servicios/dialog.service';

@Component({
  selector: 'app-lista-productos-compra',
  templateUrl: './lista-productos-compra.component.html',
  styleUrls: ['./lista-productos-compra.component.css'],
})
export class ListaProductosCompraComponent implements OnInit {
  products: Product[] = [];;

  constructor(
    private productService: ProductService,
    private cartService: CartService,
    private dialogService: DialogService
  ) {}

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
        this.dialogService.openAlertDialog('Error', 'Error al obtener la lista de productos. Intente nuevamente.');
      }
    );
  }

  addToCart(product: Product) {
    this.cartService.addToCart(product).subscribe(
      () => {
        // Manejo en caso de éxito
        this.dialogService.openAlertDialog('Éxito', 'Producto agregado al carrito correctamente.');
      },
      (error) => {
        // Manejo de errores
        console.error('Error al agregar producto al carrito:', error);
        this.dialogService.openAlertDialog('Error', 'Error al agregar el producto al carrito. Intente nuevamente.');
      }
    );
  }
  

  removeFromCart(product: Product) {
    this.cartService.removeFromCart(product).subscribe(
      () => {
        // Manejo en caso de éxito
        this.dialogService.openAlertDialog('Éxito', 'Producto eliminado del carrito correctamente.');
      },
      (error) => {
        // Manejo de errores
        console.error('Error al eliminar producto del carrito:', error);
        this.dialogService.openAlertDialog('Error', 'Error al eliminar el producto del carrito. Intente nuevamente.');
      }
    );
  }
}
