using CoffeeShop.DataAccess.DataAccess.ModelDB.ProductModel;
using System.ComponentModel.DataAnnotations.Schema;


namespace CoffeeShop.DataAccess.DataAccess.ModelDB.Order
{
    public class Order
    {
        public Guid Id { get; set; }
      
        [ForeignKey("ProductsId")]
        public List<Product> Products { get; set; }
        public Guid AddressId { get; set; }

        [ForeignKey("AddressId ")]
        public Address Address { get; set; }
        public int TotalPrices { get; set; }
        public string Currency { get; set; }

    }
}
