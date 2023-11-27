using CoffeeShop.DataAccess.DataAccess.ModelDB.ProductModel;
using System.ComponentModel.DataAnnotations.Schema;


namespace CoffeeShop.DataAccess.DataAccess.ModelDB.Order
{
    public class Order
    {
        public Guid Id { get; set; }
        //public List<Guid> ProductsId { get; set; }

        //[ForeignKey("ProductsId")]
        //public List<Product> Products { get; set; }
        public Guid AndressId { get; set; }

        [ForeignKey("AndressId")]
        public Address Address { get; set; }
        public int TotalPrices { get; set; }
        public string Currency { get; set; }

    }
}
