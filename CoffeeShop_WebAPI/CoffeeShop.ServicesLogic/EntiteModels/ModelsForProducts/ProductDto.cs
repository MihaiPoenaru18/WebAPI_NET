
namespace CoffeeShop.ServicesLogic.EntiteModels.ModelsForProducts
{
    public class ProductDto
    {
        public string Name { get; set; } = string.Empty;
        public string Sku { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Currency { get; set; } = string.Empty;
        public int Price { get; set; }
        public int Quantity { get; set; }
        public bool IsStock { get; set; }
        public PromotionDto Promotion { get; set; }
        public CategoryDto Category { get; set; }
        public string ImagePath { get; set; }
    }
}
