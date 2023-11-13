import { Component, OnInit } from '@angular/core';
import { ProductService } from 'src/app/servicios/product.service';
import { Product } from 'src/app/modelos/Product';
import { CartService } from 'src/app/servicios/cart.service';
import { DialogService } from 'src/app/servicios/dialog.service';
import { Purchase } from '../../modelos/Purchase';
import { PurchaseService } from '../../servicios/purchase.service';
import { PurchaseProduct } from '../../modelos/PurchaseProduct';


@Component({
  selector: 'app-carrito',
  templateUrl: './carrito.component.html',
  styleUrls: ['./carrito.component.css']
})
export class CarritoComponent implements OnInit {
  
  purchasedProducts: PurchaseProduct[] = [];
  products:Product[]=[];
  user: any = { rol: '' };
  fecha: Date = new Date();
  purchase:Purchase=new Purchase(0,0,this.user,this.purchasedProducts,'',this.fecha,'');
  constructor(
    private productService: ProductService,
    private cartService: CartService,
    private dialogService: DialogService,
    private purchaseService:PurchaseService
  ) {}

  ngOnInit(): void {
    this.cartService.getCart();
  }

  removeFromCart(product: Product) {
    this.cartService.removeFromCart(product).subscribe(
      () => {
        // Manejo en caso de éxito
        this.dialogService.openAlertDialog('Éxito', 'Producto eliminado del carrito correctamente');
      },
      (error) => {
        // Manejo de errores
        console.error('Error al eliminar producto del carrito:', error);
        this.dialogService.openAlertDialog('Error', 'Error al eliminar el producto del carrito');
      }
    );
  }

  registerPurchase(){

    this.purchaseService.registerPurchase(this.purchase);
  }

}
