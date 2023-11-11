import { Product } from "./Product";
import { User } from "src/app/modelos/User";
import { Purchase } from "./Purchase";


export class PurchaseProduct {
    purchaseID: number;
    purchase: Purchase;
    productID: number;
    product: Product;

    constructor(purchaseID:number,purchase:Purchase,productID:number,product:Product){
        this.purchaseID=purchaseID;
        this.purchase=purchase;
        this.productID=productID;
        this.product=product;
    }

}