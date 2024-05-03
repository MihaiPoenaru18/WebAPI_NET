import { ProductInterfaces } from "../Product-Page/product.interfaces";

export interface address {
    street: string;
    city: string;
    region: string;
    state: string;
    postalCode: string;
    country: string;
}
export interface OrderInterfaces {
    products: ProductInterfaces[];
    address: address;
    totalPrices: number;
    currency: string;
    status: number;
    userId: string;
}
