using AutoMapper;
using CoffeeShop.DataAccess.DataAccess.ModelDB.OrderModels;
using CoffeeShop.DataAccess.DataAccess.ModelDB.ProductModel;
using CoffeeShop.DataAccess.DataAccess.Repository.Interfaces;
using CoffeeShop.ServicesLogic.EntiteModels;
using CoffeeShop.ServicesLogic.Services.InterfacesServices;
using CoffeeShop_WebApi.Services.AutoMapper;
using Serilog;

namespace CoffeeShop.ServicesLogic.Services
{
    public class ServicesOrder : IServicesOrder<OrderDto>
    {

        private ICoffeeShopOrderRepository<Order> _repositoryOrder;
        private ICoffeeShopProductsRepository<Product> _repositoryProduct;
        private readonly IMapper _mapper;
        public ServicesOrder(ICoffeeShopOrderRepository<Order> repository, IMapper mapper, ICoffeeShopProductsRepository<Product> repositoryProduct)
        {
            _repositoryOrder = repository;
            _mapper = mapper;
            _repositoryProduct = repositoryProduct;
        }

        public bool DeleteOrder(OrderDto order)
        {
            var mappeOrder = MapperConfig<OrderDto, Order>.InitializeAutomapper();
            var isFinishProcess = false;
            try
            {
                if (order != null)
                {
                        if (IsOrderExistInDb(order.Id))
                        {
                            _repositoryOrder.DeleteById(mappeOrder.Map<OrderDto, Order>(order).OrderId);
                            isFinishProcess = true;
                        }
                    
                    return isFinishProcess;
                }
                return isFinishProcess;
            }
            catch (Exception ex)
            {
                Log.Error($"ServicesProducts  -> DeleteOrder() -> Exception => {ex.Message}");
                return isFinishProcess;
            }
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrders()
        {
            var mappeOrders = MapperConfig<Order, OrderDto>.InitializeAutomapper();
            var ordersDto = new List<OrderDto>();
            try
            {
                var orders = _repositoryOrder.GetAll().Result;
                if (orders != null || orders.Count() > 0)
                {   
                    foreach (var order in orders)
                    {  
                        ordersDto.Add(mappeOrders.Map<Order, OrderDto>(order));
                    }
                }
                return ordersDto;
            }
            catch (Exception ex)
            {
                Log.Error("ServicesProducts  -> GetAllOrders() -> Exception => {@ex.Message}", ex.Message);
            }
            return null;
        }

        public OrderDto GetOrder(Guid orderId)
        {
            var mappeOrder = MapperConfig<Order, OrderDto>.InitializeAutomapper();
            try
            {
                if (orderId == null)
                {
                    throw new ArgumentException("Product Name is null or empty", nameof(orderId));
                }

                return  mappeOrder.Map<Order, OrderDto>(_repositoryOrder.GetAll().Result.FirstOrDefault(p => p.OrderId == orderId));
            }
            catch (Exception ex)
            {
                Log.Error($"ServicesOrder-> GetOrder() -> Exception => {ex.Message}");
                return null;
            }
        }

        public bool AddNewOrder(OrderDto order)
        {
            var mappeProducts = MapperConfig<OrderDto, Order>.InitializeAutomapper();
            var finishInsert = false;
            try
            {
                if (order != null)
                {
                    if (!IsOrderExistInDb(order.Id))
                    {
                       finishInsert = _repositoryOrder.Insert(mappeProducts.Map<OrderDto, Order>(order)).Result;
                    }
                    return finishInsert;
                }
                else
                {
                    throw new NullReferenceException("OrderID is null!!!");
                }
            }
            catch (Exception ex)
            {
                Log.Error($"ServicesProducts  -> (OrderDto order)() -> Exception => {ex.Message}");
                return false;
            }
        }

        public bool IsOrderExistInDb(Guid orderId)
        {
            try
            {
                if (orderId == null)
                {
                    throw new ArgumentException("Product Name is null or empty", nameof(orderId));
                }
                var order = _repositoryOrder.GetAll().Result;
                return order != null ? order.Any(p => p.OrderId == orderId) : true;
            }
            catch (Exception ex)
            {
                Log.Error($"ServicesOrder-> IsOrderExistInDb() -> Exception => {ex.Message}");
                return false;
            }
        }

        public async Task UpdateOrder(OrderDto order)
        {
            try
            {
                var mappeOrder = MapperConfig<OrderDto, Order>.InitializeAutomapper();
                _repositoryOrder.Update(mappeOrder.Map<OrderDto, Order>(order));
            }
            catch (Exception ex)
            {
                Log.Error("ServicesProducts  -> UpdateOrder() -> Exception => {@ex.Message}", ex.Message);
            }
        }
    }
}
