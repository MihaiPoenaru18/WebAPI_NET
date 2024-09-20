using AutoMapper;
using CoffeeShop.DataAccess.DataAccess.ModelDB.OrderModels;
using CoffeeShop.DataAccess.DataAccess.ModelDB.ProductModel;
using CoffeeShop.DataAccess.DataAccess.Repository.Interfaces;
using CoffeeShop.ServicesLogic.EntiteModels;
using CoffeeShop.ServicesLogic.Services.InterfacesServices;
using Serilog;

namespace CoffeeShop.ServicesLogic.Services
{
    public class ServicesOrder : IServicesOrder<OrderDto>
    {
        private readonly ICoffeeShopOrderRepository<Order> _orderRepository;
        private readonly ICoffeeShopProductsRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public ServicesOrder(ICoffeeShopOrderRepository<Order> orderRepository, IMapper mapper, ICoffeeShopProductsRepository<Product> productRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<bool> DeleteOrder(Guid orderId)
        {
            try
            {
                if (await IsOrderExistInDb(orderId))
                {
                    await _orderRepository.DeleteById(orderId);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error deleting order with ID: {OrderId}", orderId);
                return false;
            }
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrders()
        {
            try
            {
                var orders = await _orderRepository.GetAll();
                return _mapper.Map<IEnumerable<OrderDto>>(orders);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error fetching all orders");
                return Enumerable.Empty<OrderDto>();
            }
        }

        public async Task<OrderDto> GetOrder(Guid orderId)
        {
            try
            {
                var order = await _orderRepository.GetById(orderId);
                return _mapper.Map<OrderDto>(order);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error fetching order with ID: {OrderId}", orderId);
                return new OrderDto();
            }
        }

        public async Task<bool> AddNewOrder(OrderDto orderDto)
        {
            if (orderDto == null)
            {
                Log.Warning("OrderDto is null");
                throw new ArgumentNullException(nameof(orderDto));
            }

            try
            {
                if (!await IsOrderExistInDb(orderDto.Id))
                {
                    var order = _mapper.Map<Order>(orderDto);
                    return await _orderRepository.Insert(order);
                }
                return false;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error adding new order");
                return false;
            }
        }

        public async Task<bool> IsOrderExistInDb(Guid orderId)
        {
            try
            {
                var order = await _orderRepository.GetById(orderId);
                return order != null;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error checking existence of order with ID: {OrderId}", orderId);
                return false;
            }
        }

        public async Task UpdateOrder(OrderDto orderDto)
        {
            if (orderDto == null)
            {
                Log.Warning("OrderDto is null in UpdateOrder");
                throw new ArgumentNullException(nameof(orderDto));
            }

            try
            {
                var order = _mapper.Map<Order>(orderDto);
                await _orderRepository.Update(order);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error updating order with ID: {OrderId}", orderDto.Id);
                throw; // Re-throwing the exception to allow higher-level handling if needed
            }
        }
    }
}
