import { Injectable } from '@angular/core';
import { ProductInterfaces } from 'src/app/componets/Product-Page/products-list/product.interfaces';


@Injectable({
  providedIn: 'root'
})
export class ProductConversionTypeService {

  constructor() { }
  
  convertFromBEToFE(beProduct: any): ProductInterfaces {
    return {
      ...beProduct,
      price: beProduct.price,
      name: beProduct.name
      
    }
  }


  convertProductsListFromBEtoFE(beProducts:any[]): ProductInterfaces[]{
    // const result: ProductInterfaces[] =  [];

    //  beProducts.forEach(product =>  
    //   {
    //       result.push(this.convertFromBEToFE(product));
    // });
    // return result;

    // cleaner code version
    return beProducts.map(this.convertFromBEToFE);

  }



}
