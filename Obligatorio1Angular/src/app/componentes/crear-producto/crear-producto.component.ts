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
  product:Product=new Product(0,'',0,'',0,0,'',0);

  constructor(private producrService: ProductService, private router: Router, private dialogService: DialogService, private localStorageService: LocalStorageService) {}

  onSubmit():void {
    
  }
}
