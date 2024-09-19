using AutoMapper;
using CoffeeShop.DataAccess.DataAccess.ModelDB.OrderModels;
using CoffeeShop.DataAccess.DataAccess.ModelDB.ProductModel;
using CoffeeShop.DataAccess.DataAccess.Repository.Interfaces;
using CoffeeShop.ServicesLogic.EntiteModels;
using CoffeeShop.ServicesLogic.Services.InterfacesServices;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShop.ServicesLogic.Services
{
    public class ServicesOrder : IServicesOrder<OrderDto>
    {
        private readonly ICoffeeShopOrderRepository<Order> _repositoryOrder;
        private readonly ICoffeeShopProductsRepository<Product> _repositoryProduct;
        private readonly IMapper _mapper;

        public ServicesOrder(ICoffeeShopOrderRepository<Order> repository, IMapper mapper, ICoffeeShopProductsRepository<Product> repositoryProduct)
        {
            _repositoryOrder = repository;
            _mapper = mapper;
            _repositoryProduct = repositoryProduct;
        }

        public async Task<bool> DeleteOrder(Guid orderId)
        {
            try
            {
                if (await IsOrderExistInDb(orderId))
                {
                    await _repositoryOrder.DeleteById(orderId);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Log.Error($"ServicesOrder -> DeleteOrder() -> Exception => {ex.Message}");
                return false;
            }
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrders()
        {
            try
            {
                var orders = await _repositoryOrder.GetAll();
                return _mapper.Map<IEnumerable<OrderDto>>(orders);
            }
            catch (Exception ex)
            {
                Log.Error($"ServicesOrder -> GetAllOrders() -> Exception => {ex.Message}");
                return Enumerable.Empty<OrderDto>();
            }
        }

        public async Task<OrderDto> GetOrder(Guid orderId)
        {
            try
            {
                var order = await _repositoryOrder.GetById(orderId);
                return _mapper.Map<OrderDto>(order);
            }
            catch (Exception ex)
            {
                Log.Error($"ServicesOrder -> GetOrder() -> Exception => {ex.Message}");
                return null;
            }
        }

        public async Task<bool> AddNewOrder(OrderDto orderDto)
        {
            try
            {
                if (orderDto == null)
                {
                    throw new ArgumentNullException(nameof(orderDto));
                }

                if (!await IsOrderExistInDb(orderDto.Id))
                {
                    var order = _mapper.Map<Order>(orderDto);
                    return await _repositoryOrder.Insert(order);
                }
                return false;
            }
            catch (Exception ex)
            {
                Log.Error($"ServicesOrder -> AddNewOrder() -> Exception => {ex.Message}");
                return false;
            }
        }

        public async Task<bool> IsOrderExistInDb(Guid orderId)
        {
            try
            {
                var order = await _repositoryOrder.GetById(orderId);
                return order != null;
            }
            catch (Exception ex)
            {
                Log.Error($"ServicesOrder -> IsOrderExistInDb() -> Exception => {ex.Message}");
                return false;
            }
        }

        public async Task UpdateOrder(OrderDto orderDto)
        {
            try
            {
                var order = _mapper.Map<Order>(orderDto);
                await _repositoryOrder.Update(order);
            }
            catch (Exception ex)
            {
                Log.Error($"ServicesOrder -> UpdateOrder() -> Exception => {ex.Message}");
                throw; // Rethrow the exception after logging
            }
        }
    }
}