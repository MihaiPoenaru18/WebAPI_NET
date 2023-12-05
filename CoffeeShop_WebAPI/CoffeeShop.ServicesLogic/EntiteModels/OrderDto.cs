using CoffeeShop.DataAccess.DataAccess.ModelDB.ProductModel;


namespace CoffeeShop.ServicesLogic.EntiteModels
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public List<Product> Products { get; set; }
        public Guid AndressId { get; set; }
        public AddressDto Address { get; set; }
        public int TotalPrices { get; set; }
        public string Currency { get; set; }
    }
}
