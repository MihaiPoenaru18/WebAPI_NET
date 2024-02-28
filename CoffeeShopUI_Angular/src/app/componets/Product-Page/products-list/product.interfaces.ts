export interface ProductInterfaces {
  name: string;
  sku: string;
  description: string;
  currency: string;
  price: number;
  quantity: number;
  isStock: boolean;
  imagePath: string;
  promotion: PromotionInterfaces;
  category: CategoryInterfaces;
}

export interface PromotionInterfaces  {
  
  pricePromotion: number;
  startDate: string;
  endDate: string;
  
}

export interface CategoryInterfaces {
  name: string;
}