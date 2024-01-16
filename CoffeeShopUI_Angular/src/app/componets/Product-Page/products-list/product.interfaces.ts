export interface ProductInterfaces {
  Name: string;
  Sku: string;
  Description: string;
  Currency: string;
  Price: number;
  Quantity: number;
  IsStock: boolean;
  Promotion?: PromotionInterfaces;
  Category: CategoryInterfaces;
}

export interface PromotionInterfaces {
  PricePromotion: number;
  StartDate: string;
  EndDate: string;
}

export interface CategoryInterfaces {
  Name: string;
}