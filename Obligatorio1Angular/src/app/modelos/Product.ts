export class Product {
    productID: number;
    name: string;
    price: number;
    description: string;
    brand: number;
    category: number;
    color: string;
    

    constructor (productID: number, name: string, price: number, description: string, brand: number, category: number, 
        color: string ){

        this.productID = productID;
        this.name = name;
        this.price = price;
        this.description = description;
        this.brand = brand;
        this.category = category;
        this.color = color;
        
    }
    };