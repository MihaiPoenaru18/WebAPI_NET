using CoffeeShop.ServicesLogic.EntiteModels;
using CoffeeShop.ServicesLogic.Services.InterfacesServices;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CoffeeShop_WebApi.Controllers.Product
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IServicesOrder<OrderDto> _services;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IServicesOrder<OrderDto> services, ILogger<OrderController> logger)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("{orderId:guid}")]
        public async Task<ActionResult<OrderDto>> GetOrder(Guid orderId)
        {
            if (orderId == Guid.Empty)
            {
                return BadRequest("Invalid order ID.");
            }

            try
            {
                var order = await _services.GetOrder(orderId);
                return order != null ? Ok(order) : NotFound("Order not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting order with ID: {OrderId}", orderId);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddOrder([FromBody] OrderDto order)
        {
            if (order == null)
            {
                return BadRequest("Order data is required.");
            }

            try
            {
                var result = await _services.AddNewOrder(order);
                return result ? CreatedAtAction(nameof(GetOrder), new { orderId = order.Id }, order) : BadRequest("Failed to add order.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding new order");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllOrders()
        {
            try
            {
                var orders = await _services.GetAllOrders();
                return orders.Any() ? Ok(orders) : NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all orders");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpDelete("{orderId:guid}")]
        public async Task<ActionResult> DeleteOrder(Guid orderId)
        {
            if (orderId == Guid.Empty)
            {
                return BadRequest("Invalid order ID.");
            }

            try
            {
                var result = await _services.DeleteOrder(orderId);
                return result ? NoContent() : NotFound("Order not found or deletion failed.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting order with ID: {OrderId}", orderId);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpPut("{orderId:guid}")]
        public async Task<ActionResult> UpdateOrder(Guid orderId, [FromBody] OrderDto order)
        {
            if (orderId == Guid.Empty || order == null || orderId != order.Id)
            {
                return BadRequest("Invalid order data or mismatched IDs.");
            }

            try
            {
                await _services.UpdateOrder(order);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating order with ID: {OrderId}", orderId);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }
    }
}