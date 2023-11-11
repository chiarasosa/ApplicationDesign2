import { Product } from "./Product";
import { Cart } from "./Cart";

export class CartProduct {
    cartID: number;
    cart: Cart;
    productID: number;
    product: Product;


    constructor(cartID: number, cart: Cart, productID: number, product: Product) {
        this.cartID = cartID;
        this.cart = cart;
        this.productID = productID;
        this.product = product;
    }
}

