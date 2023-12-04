using CoffeeShop.ServicesLogic.EntiteModels.ModelsForProducts;
using CoffeeShop.ServicesLogic.Services.Interfaces;
using CoffeeShop_WebApi.QuerybleCustomMethod;
using CoffeeShop_WebApi.QuerybleCustomMethod.ModelOfParameters;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CoffeeShop_WebApi.Controllers.Product
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IServicesProduct<ProductDto> _services;
        public ProductController(IServicesProduct<ProductDto> services)
        {
            _services = services;
        }

        [HttpGet("GetCategories")]
        public ActionResult<IEnumerable<CategoryDto>> GetCategories()
        {
            try
            {
                var categories = _services.GetAllCategories().Result.OrderBy(c=>c.Name).ToList();
                if (categories == null)
                {
                    return BadRequest("Not category in database");
                }
                return Ok(categories);
            }
            catch (Exception ex)
            {
                Log.Error("ProductController -> GetCategories() -> Exception => {@ex.Message}", ex.Message);
                return BadRequest("Error");
            }
        }
        
        [HttpGet("GetProducts")]
        public ActionResult<IEnumerable<ProductDto>> GetProducts([FromQuery] ProductQueryParameters parameters)
        {
            try
            {
                var products = _services.GetAllProducts().Result;
                if (products == null)
                {
                    return BadRequest("Not products in database");
                }
               
                if( parameters.MaxPrice == null && parameters.MinPrice == null)
                {
                    products = products.Skip(parameters.Size * (parameters.Page - 1)).Take(parameters.Size);
                }

                if(parameters.MinPrice != null)
                {
                    products = products.Where(p=>p.Price > parameters.MinPrice).ToList();
                }

                if(parameters.MaxPrice != null)
                {
                    products = products.Where(p=>p.Price < parameters.MaxPrice).ToList();
                }

                if (!String.IsNullOrEmpty(parameters.SortBy))
                {
                    if(typeof(ProductDto).GetProperty(parameters.SortBy) != null)
                    {
                        products = products.OrderByProperty(parameters.SortBy, parameters.SortOrder);
                    }
                }

                if(!String.IsNullOrEmpty(parameters.SearchTerm)) 
                {
                    products = products.SerachBy(parameters.SearchTerm);
                }

                return Ok(products);
            }
            catch (Exception ex)
            {
                Log.Error("ProductController -> GetProducts() -> Exception => {@ex.Message}", ex.Message);
                return BadRequest("Error");
            }
        }

        [HttpPost("AddProducts")]
        public ActionResult AddProducts([FromBody] List<ProductDto> products)
        {
            try
            {
                if (_services.AddNewProducts(products))
                {
                    return Ok("Products add in DB with success");
                }
                return BadRequest("Your products don't was add in db");
            }
            catch (Exception ex)
            {
                Log.Error($"ProductController -> GetProducts() -> Exception => {ex.Message}");
                return BadRequest("Error");
            }
        }

        [HttpPost("DeleteProducts")]
        public ActionResult DeleteProducts([FromBody] List<ProductDto> products)
        {
            try
            {
                if (_services.DeleteProduct(products))
                {
                    return Ok("Products was delete from DB with success");
                }
                return BadRequest("Your products don't was delete from db");
            }
            catch (Exception ex)
            {
                Log.Error($"ProductController -> DeleteProducts() -> Exception => {ex.Message}");
                return BadRequest("Error");
            }
        }

        [HttpPut("UpdateProduct")]
        public ActionResult UpdateProduct([FromBody] ProductDto product)
        {
            try
            {
                _services.UpdateProductInformation(product);
                return Ok("Product update with success");
            }
            catch (Exception ex)
            {
                Log.Error("ProductController -> GetProducts() -> Exception => {@ex.Message}", ex.Message);
                return BadRequest("Error");
            }
        }
    }
}
