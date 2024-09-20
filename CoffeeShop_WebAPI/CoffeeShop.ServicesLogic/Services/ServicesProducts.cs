using AutoMapper;
using CoffeeShop.DataAccess.DataAccess.ModelDB.ProductModel;
using CoffeeShop.DataAccess.DataAccess.Repository.Interfaces;
using CoffeeShop.ServicesLogic.EntiteModels.ModelsForProducts;
using CoffeeShop.ServicesLogic.Services.Interfaces;
using Serilog;

namespace CoffeeShop.ServicesLogic.Services
{
    public class ServicesProducts : IServicesProduct<ProductDto>
    {
        private readonly IMapper _mapper;
        private readonly ICoffeeShopProductsRepository<Product> _coffeeShopProductRepository;

        public ServicesProducts(ICoffeeShopProductsRepository<Product> coffeeShopProductRepository, IMapper mapper)
        {
            _mapper = mapper;
            _coffeeShopProductRepository = coffeeShopProductRepository;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProducts()
        {
            try
            {
                var products = await _coffeeShopProductRepository.GetAll();
                return _mapper.Map<IEnumerable<ProductDto>>(products);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error fetching all products");
                return Enumerable.Empty<ProductDto>();
            }
        }

        public async Task UpdateProductInformation(ProductDto product)
        {
            try
            {
                var mappedProduct = _mapper.Map<Product>(product);
                await _coffeeShopProductRepository.Update(mappedProduct);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error updating product information for {ProductName}", product.Name);
            }
        }

        public async Task<bool> IsProductExistingInDb(string productName)
        {
            if (string.IsNullOrWhiteSpace(productName))
            {
                Log.Warning("Product name is empty or null");
                return false;
            }

            try
            {
                var products = await _coffeeShopProductRepository.GetAll();
                return products.Any(p => p.Name == productName);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error checking if product {ProductName} exists in DB", productName);
                return false;
            }
        }

        public async Task<bool> AddNewProducts(List<ProductDto> products)
        {
            if (products == null || !products.Any())
            {
                Log.Warning("No products provided to add");
                return false;
            }

            try
            {
                foreach (var product in products)
                {
                    if (!await IsProductExistingInDb(product.Name))
                    {
                        var mappedProduct = _mapper.Map<Product>(product);
                        await _coffeeShopProductRepository.Insert(mappedProduct);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error adding new products");
                return false;
            }
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategories()
        {
            try
            {
                var categories = await _coffeeShopProductRepository.GetAllCategories();
                return _mapper.Map<IEnumerable<CategoryDto>>(categories).DistinctBy(c => c.Name);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error fetching all categories");
                return Enumerable.Empty<CategoryDto>();
            }
        }

        public async Task<bool> DeleteProducts(List<ProductDto> products)
        {
            if (products == null || !products.Any())
            {
                Log.Warning("No products provided for deletion");
                return false;
            }

            try
            {
                foreach (var product in products)
                {
                    if (await IsProductExistingInDb(product.Name))
                    {
                        await _coffeeShopProductRepository.Delete(product.Name);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error deleting products");
                return false;
            }
        }
    }
}
