import { PurchaseProduct } from "./PurchaseProduct";
import { User } from "./User";

export class Purchase {
    purchaseID: number;
    userID: number;
    user: User;
    userName: string;
    purchasedProducts: PurchaseProduct[];
    promoApplied: string;
    dateOfPurchase: Date | null;
    paymentMethod: string;
    emailUsuario: string;

    constructor(purchaseID:number,userID:number,user:User,purchasedProducts:PurchaseProduct[],promoApplied:string,dateOfPurchase:Date,paymentMethod:string, 
                userName:string, emailUsuario:string){
        this.purchaseID=purchaseID;
        this.userID=userID;
        this.user=user;
        this.purchasedProducts=purchasedProducts;
        this.promoApplied=promoApplied;
        this.dateOfPurchase=dateOfPurchase;
        this.paymentMethod=paymentMethod;
        this.userName= userName;
        this.emailUsuario= emailUsuario;
    }
}