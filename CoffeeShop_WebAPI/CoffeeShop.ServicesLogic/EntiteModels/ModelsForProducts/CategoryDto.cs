

namespace CoffeeShop.ServicesLogic.EntiteModels.ModelsForProducts
{
    public class CategoryDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
    }
}
