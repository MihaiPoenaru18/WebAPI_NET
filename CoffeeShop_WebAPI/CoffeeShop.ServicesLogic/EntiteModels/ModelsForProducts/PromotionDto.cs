
using System.Text.Json.Serialization;

namespace CoffeeShop.ServicesLogic.EntiteModels.ModelsForProducts
{
    public class PromotionDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int PricePromotion { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
