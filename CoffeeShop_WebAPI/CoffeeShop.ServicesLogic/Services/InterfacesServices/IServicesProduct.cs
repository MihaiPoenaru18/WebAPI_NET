﻿using CoffeeShop.ServicesLogic.EntiteModels.ModelsForProducts;

namespace CoffeeShop.ServicesLogic.Services.Interfaces
{
    public interface IServicesProduct<T> where T : class
    {
        Task<bool> IsProductExistingInDb (string productName);
        Task<IEnumerable<T>> GetAllProducts();
        Task<IEnumerable<CategoryDto>> GetAllCategories ();
        bool AddNewProducts(List<T> products); 
        void UpdateProductInformation(T product);
        bool DeleteProduct(List<T> product);

    }
}
