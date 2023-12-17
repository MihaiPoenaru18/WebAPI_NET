using System.Text.Json.Serialization;

namespace CoffeeShop.DataAccess.DataAccess.ModelDB.ProductModel
{
    public class Category
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }

        [JsonIgnore]
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
