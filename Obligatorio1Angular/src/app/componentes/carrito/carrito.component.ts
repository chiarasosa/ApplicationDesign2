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
  promotionData: any;
  constructor(
    private productService: ProductService,
    private cartService: CartService,
    private dialogService: DialogService,
    private purchaseService:PurchaseService
  ) {}

  ngOnInit(): void {
    this.getCarts();
    this.getPromotionData();
  }

  removeFromCart(product: Product) {
    this.cartService.removeFromCart(product).subscribe(
      () => {
        // Manejo en caso de éxito
        this.dialogService.openAlertDialog('Éxito', 'Producto eliminado del carrito correctamente');
        // Refrescar la lista de carritos después de eliminar un producto
        this.getCarts();
      },
      (error) => {
        // Manejo de errores
        console.error('Error al eliminar producto del carrito:', error);
        this.dialogService.openAlertDialog('Error', 'Error al eliminar el producto del carrito');
      }
    );
  }
  
  getPromotionData() {
    this.cartService.getPromotionAppliedToCart().subscribe(
      (data) => {
        this.promotionData = data.message;
        // Hacer algo con los datos recibidos, por ejemplo, asignar a una variable en tu componente
        console.log('Promoción recibida:', data);
      },
      (error) => {
        console.error('Error al obtener la promoción:', error);
        // Manejar el error, mostrar un mensaje o hacer algo en caso de error
      }
    );
  }

  getCarts() {
    this.cartService.getCart().subscribe(
      (products) => {
        // Actualizar la lista de carritos
        this.products = products;
      },
      (error) => {
        console.error('Error al obtener la lista de carritos:', error);
        this.dialogService.openAlertDialog('Error', 'Error al obtener la lista de carritos');
      }
    );
  }
  
  getTotalPrice() {
    if (this.products && this.products.length > 0) {
      return this.products.reduce((total, product) => total + product.price, 0);
    }
    return 0; 
  }

  registerPurchase(){

    this.purchaseService.registerPurchase(this.purchase).subscribe(
      (response) => {
        // Maneja la respuesta del inicio de sesión aquí
        console.log('Compra registrada:', response);
       
        this.openAlertDialog('Éxito', 'Compra realizada con exito.');
 
       
      },
      (error) => {
        console.error('Error al registrar la compra:', error);
        this.openAlertDialog('Error', 'Error al registrar la compra. Intente nuevamente.');
      }
    );
  }

  openAlertDialog(title: string, message: string) {
    this.dialogService.openAlertDialog(title, message); // Usa dialogService
  }

  

}
