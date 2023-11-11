import { Product } from "./Product";
import { User } from "../interfaces/user.model";
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