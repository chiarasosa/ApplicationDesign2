import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/modelos/Product';
import { ProductService } from 'src/app/servicios/product.service';


@Component({
  selector: 'app-lista-productos',
  templateUrl: './lista-productos.component.html',
  styleUrls: ['./lista-productos.component.css']
})
export class ListaProductosComponent implements OnInit {
  products: Product[] | undefined;

  constructor(private productService: ProductService) { }

  ngOnInit() {
    this.productService.getProducts().subscribe((products: Product[]) => {
      this.products = products;
    });
}
}
