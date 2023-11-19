using CoffeeShop.ServicesLogic.EntiteModels.ModelsForProducts;
using CoffeeShop.ServicesLogic.Services.Interfaces;

namespace CoffeeShop.ServicesLogic.Services
{
    public class ServicesProducts : IServicesProduct<ProductDto>
    {
        public Task<IEnumerable<ProductDto>> GetAllProducts()
        {
            throw new NotImplementedException();
        }

        public bool IsProductExistingInDb(string productName)
        {
            throw new NotImplementedException();
        }

        public void UpdateProductInformation(ProductDto product)
        {
            throw new NotImplementedException();
        }
    }
}
