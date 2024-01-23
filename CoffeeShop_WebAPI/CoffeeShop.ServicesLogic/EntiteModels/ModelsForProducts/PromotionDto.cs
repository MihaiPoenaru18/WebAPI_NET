
using System.Text.Json.Serialization;

namespace CoffeeShop.ServicesLogic.EntiteModels.ModelsForProducts
{
    public class PromotionDto
    {
        public int PricePromotion { get; set; }
        public String StartDate { get; set; }
        public String EndDate { get; set; }

    }
}
