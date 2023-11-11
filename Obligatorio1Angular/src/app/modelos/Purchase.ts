import { PurchaseProduct } from "./PurchaseProduct";
import { User } from "./User";

export class Purchase {
    purchaseID: number;
    userID: number;
    user: User;
    purchasedProducts: PurchaseProduct[];
    promoApplied: string;
    dateOfPurchase: Date | null;
    paymentMethod: string;

    constructor(purchaseID:number,userID:number,user:User,purchasedProducts:PurchaseProduct[],promoApplied:string,dateOfPurchase:Date,paymentMethod:string){
        this.purchaseID=purchaseID;
        this.userID=userID;
        this.user=user;
        this.purchasedProducts=purchasedProducts;
        this.promoApplied=promoApplied;
        this.dateOfPurchase=dateOfPurchase;
        this.paymentMethod=paymentMethod;
    }
}