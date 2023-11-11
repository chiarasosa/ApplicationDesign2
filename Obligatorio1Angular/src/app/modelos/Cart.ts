
import { Product } from "./Product";
import { CartProduct } from "./CartProduct";

export class Cart {
    cartID: number;
    cartProducts: CartProduct[];
    products: Product[]; // Esta propiedad no se mapea a la base de datos
    totalPrice: number;
    promotionApplied: string;
    userID: number;

    constructor(cartID:number,cartProducts:CartProduct[],products:Product[],totalPrice:number,promotionApplied:string,userID:number) {
        
        this.cartID=cartID;
        this.products=products;
        this.userID=userID;
        this.cartProducts = cartProducts;
        this.totalPrice = totalPrice;
        this.promotionApplied = promotionApplied;
    }
}