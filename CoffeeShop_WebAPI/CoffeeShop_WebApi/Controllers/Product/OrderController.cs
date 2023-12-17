using CoffeeShop.ServicesLogic.EntiteModels;
using CoffeeShop.ServicesLogic.Services.InterfacesServices;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CoffeeShop_WebApi.Controllers.Product
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IServicesOrder<OrderDto> _services;

        public OrderController(IServicesOrder<OrderDto> services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<ActionResult<OrderDto>> GetOrder([FromQuery] Guid orderId)
        {
            try
            {
                if (orderId == Guid.Empty)
                    throw new ArgumentNullException(nameof(orderId));

                return _services.GetOrder(orderId);
            }
            catch (Exception ex)
            {
                Log.Error($"OrderController -> GetOrder()  -> Exception => {ex.Message}");
                return BadRequest("Error");
            }
        }

        [HttpPost("AddOrder")]
        public async Task<ActionResult> AddOrder([FromBody] OrderDto order)
        {
            try
            {
                if (order == null)
                {
                    throw new ArgumentNullException();
                }
                return _services.AddNewOrder(order) ? Ok("Order add with success") : BadRequest("Order fail!!");
            }
            catch (Exception ex)
            {
                Log.Error($"OrderController -> AddOrder()  -> Exception => {ex.Message}");
                return BadRequest("Error");
            }
        }

        [HttpGet("GetAllOrder")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllOrder()
        {
            try
            {
                var orders = await _services.GetAllOrders();
                return await _services.GetAllOrders() != null ? Ok(orders) : BadRequest("No orders exist in the system");
            }
            catch (Exception ex)
            {
                Log.Error($"OrderController -> GetAllOrder()  -> Exception => {ex.Message}");
                return BadRequest("Error");
            }
        }

        [HttpPost("DeleteOrder")]
        public async Task<ActionResult> DeleteOrder([FromBody] OrderDto order)
        {
            try
            {
                if (order == null)
                {
                    return BadRequest("Not order for to delete!");
                    throw new ArgumentNullException();
                }
                var result =  _services.DeleteOrder(order);
                return _services.DeleteOrder(order) ? Ok("Your order was success with succes") : BadRequest("Delete order fail!!");
            }
            catch (Exception ex)
            {
                Log.Error($"OrderController -> GetAllOrder()  -> Exception => {ex.Message}");
                return BadRequest("Error");
            }
        }

        [HttpPost("UpdateOrder")]
        public async Task<ActionResult> UpdateOrder([FromBody] OrderDto order)
        {
            try
            {
                if (order == null)
                {
                    return BadRequest("Not order for to delete!");
                    throw new ArgumentNullException();
                }
                _services.DeleteOrder(order);
                return Ok("Your order was success with succes");
            }
            catch (Exception ex)
            {
                Log.Error($"OrderController -> GetAllOrder()  -> Exception => {ex.Message}");
                return BadRequest("Error");
            }
        }
    }
}