using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CoffeeShop.DataAccess.DataAccess.ModelDB.ProductModel
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Sku { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Currency { get; set; } = string.Empty;
        public int Price { get; set; }
        public int Quantity { get; set; }
        public bool IsStock { get; set; }
        [JsonIgnore]
        public Promotion Promotion { get; set; }
        public Guid PromotionId { get; set; }
        [JsonIgnore]
        public Category Category { get; set; }
        public Guid CategoryId { get; set; }
        public string ImagePath { get; set; }
    }
}
