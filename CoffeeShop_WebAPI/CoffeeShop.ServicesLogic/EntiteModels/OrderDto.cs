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
        //public int TotalPrices
        //{
        //    get 
        //    { 
        //        return TotalPrices;
        //    }
        //    set
        //    {
        //        var sum = 0;
        //        foreach (var product in Products)
        //        { sum += product.Price; }
        //        value = sum;
        //    }
        //} 
        public string Currency { get; set; }
    }
}
