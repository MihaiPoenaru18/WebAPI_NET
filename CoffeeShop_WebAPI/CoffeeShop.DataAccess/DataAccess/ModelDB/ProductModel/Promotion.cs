namespace CoffeeShop.DataAccess.DataAccess.ModelDB.ProductModel
{
    public class Promotion
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int PricePromotion { get; set; }
        public String StartDate { get; set; }
        public String EndDate { get; set; }

    }
}
