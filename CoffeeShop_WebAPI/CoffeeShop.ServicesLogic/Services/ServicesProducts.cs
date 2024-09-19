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
                Log.Error("ServicesProducts -> GetAllProducts() -> Exception => {@ex.Message}", ex.Message);
                return null;
            }
        }

        public void UpdateProductInformation(ProductDto product)
        {
            try
            {
                var mappedProduct = _mapper.Map<Product>(product);
                _coffeeShopProductRepository.Update(mappedProduct);
            }
            catch (Exception ex)
            {
                Log.Error("ServicesProducts -> UpdateProductInformation() -> Exception => {@ex.Message}", ex.Message);
            }
        }

        public async Task<bool> IsProductExistingInDb(string productName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(productName))
                {
                    throw new ArgumentException("Product Name is null or empty", nameof(productName));
                }

                var products = await _coffeeShopProductRepository.GetAll();
                return products.Any(p => p.Name == productName);
            }
            catch (Exception ex)
            {
                Log.Error($"ServicesProducts -> IsProductExistingInDb() -> Exception => {ex.Message}");
                return false;
            }
        }

        public bool AddNewProducts(List<ProductDto> products)
        {
            var finishInsert = false;
            try
            {
                if (products != null)
                {
                    foreach (var product in products)
                    {
                        if (!IsProductExistingInDb(product.Name).Result)
                        {
                            var mappedProduct = _mapper.Map<Product>(product);
                            finishInsert = _coffeeShopProductRepository.Insert(mappedProduct).Result;
                        }
                    }
                    return finishInsert;
                }
                else
                {
                    throw new NullReferenceException("Product Name is null!!!");
                }
            }
            catch (Exception ex)
            {
                Log.Error($"ServicesProducts -> AddNewProducts() -> Exception => {ex.Message}");
                return false;
            }
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategories()
        {
            try
            {
                var categories = await _coffeeShopProductRepository.GetAllCategories();
                var categoryDtos = _mapper.Map<IEnumerable<CategoryDto>>(categories);
                return categoryDtos.DistinctBy(c => c.Name);
            }
            catch (Exception ex)
            {
                Log.Error($"ServicesProducts -> GetAllCategories() -> Exception => {ex.Message}");
                return null;
            }
        }

        public bool DeleteProduct(List<ProductDto> products)
        {
            var isFinishProcess = false;
            try
            {
                if (products != null)
                {
                    foreach (var product in products)
                    {
                        if (IsProductExistingInDb(product.Name).Result)
                        {
                            _coffeeShopProductRepository.Delete(product.Name);
                            isFinishProcess = true;
                        }
                    }
                }
                return isFinishProcess;
            }
            catch (Exception ex)
            {
                Log.Error($"ServicesProducts -> DeleteProduct() -> Exception => {ex.Message}");
                return false;
            }
        }
    }
}