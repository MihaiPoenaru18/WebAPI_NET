using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.DataAccess.DataAccess.ModelDB
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Sku { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Currency { get; set; } = string.Empty;
        public int Price { get; set; }
        public int Quantity { get; set; }
        public bool IsStock { get; set; }
        public Guid IdPromotie { get; set; }
        [ForeignKey("IdPromotie")]
        public Promotion Promotion { get; set; }
        public Guid IdCategory { get; set; }
        [ForeignKey("IdCategory")]
        public Category Category { get; set; }
    }
}
