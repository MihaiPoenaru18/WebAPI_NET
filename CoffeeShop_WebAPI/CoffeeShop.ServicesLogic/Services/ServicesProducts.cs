using AutoMapper;
using CoffeeShop.DataAccess.DataAccess.ModelDB.ProductModel;
using CoffeeShop.DataAccess.DataAccess.Repository.Interfaces;
using CoffeeShop.ServicesLogic.EntiteModels.ModelsForProducts;
using CoffeeShop.ServicesLogic.Services.Interfaces;
using CoffeeShop_WebApi.Services.AutoMapper;
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
            var mappeProducts = MapperConfig<Product, ProductDto>.InitializeAutomapper();
            var productsDto = new List<ProductDto>();
            try
            {
                var products = _coffeeShopProductRepository.GetAll().Result;
                if (products != null || products.Count() > 0)
                {
                    foreach (var product in products)
                    {
                        productsDto.Add(mappeProducts.Map<Product, ProductDto>(product));
                    }
                }
                return productsDto;
            }
            catch (Exception ex)
            {
                Log.Error("ServicesProducts  -> GetAllProducts() -> Exception => {@ex.Message}", ex.Message);
            }
            return null;
        }

        public void UpdateProductInformation(ProductDto product)
        {
            try
            {
                var mappeProducts = MapperConfig<ProductDto, Product>.InitializeAutomapper();
                _coffeeShopProductRepository.Update(mappeProducts.Map<ProductDto, Product>(product));
            }
            catch (Exception ex)
            {
                Log.Error("ServicesProducts  -> UpdateProductInformation() -> Exception => {@ex.Message}", ex.Message);
            }
        }

        public bool IsProductExistingInDb(string productName)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(productName))
                {
                    var products = _coffeeShopProductRepository.GetAll().Result;
                    if (products.Where(p => p.Name == productName).Count() > 0)
                    {
                        return true;
                    }
                }
                else
                {
                    throw new Exception("Product Name is null");
                }
                return false;
            }
            catch (Exception ex)
            {
                Log.Error($"ServicesProducts  -> IsProductExistingInDb() -> Exception => {ex.Message}");
                return false;
            }
        }

        public bool AddNewProductsInDBAsync(List<ProductDto> products)
        {
            var mappeProducts = MapperConfig<ProductDto, Product>.InitializeAutomapper();
            var finishInsert = false;
            try
            {
                if (products != null)
                {
                    foreach (var product in products)
                    {
                        if (!IsProductExistingInDb(product.Name))
                        {
                            finishInsert = _coffeeShopProductRepository.Insert(mappeProducts.Map<ProductDto, Product>(product)).Result;
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
                Log.Error($"ServicesProducts  -> GetAllUsers() -> Exception => {ex.Message}");
                return false;
            }
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategories()
        {
            var mappeCategory = MapperConfig<Category, CategoryDto>.InitializeAutomapper();
            var categoryDto = new List<CategoryDto>();
            try
            {
                var categories = _coffeeShopProductRepository.GetAllCategoris().Result;
                if (categories != null || categories.Count() > 0)
                {
                    foreach (var category in categories)
                    {
                        categoryDto.Add(mappeCategory.Map<Category, CategoryDto>(category));
                    }
                }
                return categoryDto;
            }
            catch(Exception ex)
            {
                Log.Error($"ServicesProducts  -> GetAllUsers() -> Exception => {ex.Message}");
            }
            return null;
        }

        public bool DeleteProduct(List<ProductDto> products)
        {
            var mappeProducts = MapperConfig<ProductDto, Product>.InitializeAutomapper();
            var isFinishProcess = false;
            try
            {
                if (products != null)
                {
                    foreach (var product in products)
                    {
                        if (!IsProductExistingInDb(product.Name))
                        {
                            _coffeeShopProductRepository.Delete(mappeProducts.Map<ProductDto, Product>(product).Id);
                            isFinishProcess = true;
                        }
                    }
                    return isFinishProcess;
                }
                return isFinishProcess;
            }
            catch (Exception ex)
            {
                Log.Error($"ServicesProducts  -> GetAllUsers() -> Exception => {ex.Message}");
                return isFinishProcess;
            }
        }
    }
}
