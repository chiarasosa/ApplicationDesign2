import { Component, OnInit } from '@angular/core';
import { ProductService } from 'src/app/servicios/product.service';
import { Product } from 'src/app/modelos/Product';
import { CartService } from 'src/app/servicios/cart.service';
import { DialogService } from 'src/app/servicios/dialog.service';
import { Purchase } from '../../modelos/Purchase';
import { PurchaseService } from '../../servicios/purchase.service';
import { PurchaseProduct } from '../../modelos/PurchaseProduct';
import { Router } from '@angular/router';
import { ChangeDetectorRef } from '@angular/core';


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
  purchase:Purchase=new Purchase(0,0,this.user,this.purchasedProducts,'',this.fecha,'', '', '');
  promotionData: any;
  selectedPaymentMethod: string | undefined;
  constructor(
    private productService: ProductService,
    private cartService: CartService,
    private dialogService: DialogService,
    private purchaseService:PurchaseService,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.getCarts();
    this.getPromotionData();
    this.selectedPaymentMethod= undefined;

    
  }

  removeFromCart(product: Product) {
    this.cartService.removeFromCart(product).subscribe(
      () => {
        this.dialogService.openAlertDialog('Éxito', 'Producto eliminado del carrito correctamente');
        this.getCarts();
      },
      (error) => {
        console.error('Error al eliminar producto del carrito:', error);
        this.cdr.detectChanges();
        this.dialogService.openAlertDialog('Error', 'Error al eliminar el producto del carrito');
      }
    );
  }
  
  getPromotionData() {
    this.cartService.getPromotionAppliedToCart().subscribe(
      (data) => {
        this.promotionData = data.message;
        console.log('Promoción recibida:', data);
      },
      (error) => {
        console.error('Error al obtener la promoción:', error);
      }
    );
  }

  getCarts() {
    this.cartService.getCart().subscribe(
      (products) => {
        this.products = products;
      },
      (error) => {
        this.products= [];
        console.error('Error al obtener la lista de carritos:', error);
        if (error != null && error.error != null && error.error.message != null) {
          this.dialogService.openAlertDialog('Error', error.error.message);

        } else {
          this.dialogService.openAlertDialog('Error', error);

        }
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
    
    if (this.selectedPaymentMethod) {
    this.purchase.paymentMethod = this.selectedPaymentMethod;
a
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
    this.openAlertDialog('Error', 'Por favor, seleccione un método de pago antes de comprar.');
  }
  }

  openAlertDialog(title: string, message: string) {
    this.dialogService.openAlertDialog(title, message);
  }

  

}
