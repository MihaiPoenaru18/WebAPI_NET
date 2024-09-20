using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class ProductController : ControllerBase
    {
        private readonly IServicesProduct<ProductDto> _services;

        public ProductController(IServicesProduct<ProductDto> services)
        {
            _services = services;
        }

        [HttpGet("GetCategories")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            try
            {
                var categories = (await _services.GetAllCategories()).OrderBy(c => c.Name).ToList();
                return categories.Any() ? Ok(categories) : NotFound("No categories found in the database.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in ProductController -> GetCategories()");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpGet("GetProducts")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts([FromQuery] ProductQueryParameters parameters)
        {
            try
            {
                var products = await _services.GetAllProducts();
                if (!products.Any())
                {
                    return NotFound("No products found in the database.");
                }

                var filteredProducts = ApplyFilters(products, parameters);
                return Ok(filteredProducts);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in ProductController -> GetProducts()");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpPost("AddProducts")]
        public async Task<ActionResult> AddProducts([FromBody] List<ProductDto> products)
        {
            if (products == null || !products.Any())
            {
                return BadRequest("Invalid product list.");
            }

            try
            {
                var productsAdded = await _services.AddNewProducts(products);
                return productsAdded
                    ? Ok("Products added to the database successfully.")
                    : Conflict("Some or all of the products already exist in the database.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in ProductController -> AddProducts()");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpPost("DeleteProducts")]
        public async Task<ActionResult> DeleteProducts([FromBody] List<ProductDto> products)
        {
            if (products == null || !products.Any())
            {
                return BadRequest("Invalid product list.");
            }

            try
            {
                var productsDeleted = await _services.DeleteProducts(products);
                return productsDeleted
                    ? Ok("Products were successfully deleted from the database.")
                    : BadRequest("Some products could not be deleted.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in ProductController -> DeleteProducts()");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpPut("UpdateProduct")]
        public async Task<ActionResult> UpdateProduct([FromBody] ProductDto product)
        {
            if (product == null)
            {
                return BadRequest("Invalid product.");
            }

            try
            {
                await _services.UpdateProductInformation(product);
                return Ok("Product updated successfully.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in ProductController -> UpdateProduct()");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        private IEnumerable<ProductDto> ApplyFilters(IEnumerable<ProductDto> products, ProductQueryParameters parameters)
        {
            if (parameters.MinPrice.HasValue)
            {
                products = products.Where(p => p.Price >= parameters.MinPrice.Value);
            }

            if (parameters.MaxPrice.HasValue)
            {
                products = products.Where(p => p.Price <= parameters.MaxPrice.Value);
            }

            if (!string.IsNullOrEmpty(parameters.SortBy) && typeof(ProductDto).GetProperty(parameters.SortBy) != null)
            {
                products = products.OrderByProperty(parameters.SortBy, parameters.SortOrder);
            }

            if (!string.IsNullOrEmpty(parameters.SearchTerm))
            {
                products = products.SerachBy(parameters.SearchTerm);
            }

            return products.Skip(parameters.Size * (parameters.Page - 1)).Take(parameters.Size);
        }
    }
}