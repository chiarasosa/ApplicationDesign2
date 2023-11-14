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
  selectedPaymentMethod: string | undefined;
  constructor(
    private productService: ProductService,
    private cartService: CartService,
    private dialogService: DialogService,
    private purchaseService:PurchaseService
  ) {}

  ngOnInit(): void {
    this.getCarts();
    this.getPromotionData();
    this.selectedPaymentMethod= undefined;

    
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
    const token = localStorage.getItem('token');
    if (!token) {
      console.error('Token no disponible'); // Agregar mensaje de error
      // Manejar el error o lanzar una alerta si es necesario
      return;
    }

    this.cartService.getPromotionAppliedToCart().subscribe(
      (data) => {
        this.promotionData = data;
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

  updatePaymentMethod(method: string) {
    this.selectedPaymentMethod = method;
  }

  registerPurchase(){
    
    console.log('Selected Payment Method:', this.selectedPaymentMethod);

    if (this.selectedPaymentMethod) {
    // Actualiza el método de pago en el objeto purchase
    this.purchase.paymentMethod = this.selectedPaymentMethod;

    // Luego procede con el registro de la compra
    this.purchaseService.registerPurchase(this.purchase,this.selectedPaymentMethod).subscribe(
      (response) => {
        console.log('Compra registrada:', response);
        this.openAlertDialog('Éxito', 'Compra realizada con éxito.');
      },
      (error) => {
        console.error('Error al registrar la compra:', error);
        this.openAlertDialog('Error', 'Error al registrar la compra. Intente nuevamente.');
      }
    );
  } else {
    // Muestra un mensaje de error si no se seleccionó un método de pago
    this.openAlertDialog('Error', 'Por favor, seleccione un método de pago antes de comprar.');
  }
  }

  openAlertDialog(title: string, message: string) {
    this.dialogService.openAlertDialog(title, message); // Usa dialogService
  }

  

}
