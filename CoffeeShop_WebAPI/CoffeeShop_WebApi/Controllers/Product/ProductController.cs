using CoffeeShop.ServicesLogic.EntiteModels.ModelsForProducts;
using CoffeeShop.ServicesLogic.Services.Interfaces;
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
                var categories = _services.GetAllCategories();
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
        public ActionResult<IEnumerable<ProductDto>> GetProducts()
        {
            try
            {
                var products = _services.GetAllProducts();
                if (products == null)
                {
                    return BadRequest("Not products in database");
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
                if (_services.AddNewProductsInDBAsync(products))
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

        [HttpPost("UpdateProduct")]
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
