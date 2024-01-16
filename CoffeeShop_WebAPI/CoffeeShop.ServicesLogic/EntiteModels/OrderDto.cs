using CoffeeShop.DataAccess.DataAccess.ModelDB.Order;
using CoffeeShop.ServicesLogic.EntiteModels.ModelsForProducts;

namespace CoffeeShop.ServicesLogic.EntiteModels
{
    public class OrderDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public List<ProductDto> Products { get; set; }
        public Guid AddressId { get; set; } = Guid.NewGuid();
        public AddressDto Address { get; set; }
        public int TotalPrices { get; set; }
        public string Currency { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Processing;
    }
}
