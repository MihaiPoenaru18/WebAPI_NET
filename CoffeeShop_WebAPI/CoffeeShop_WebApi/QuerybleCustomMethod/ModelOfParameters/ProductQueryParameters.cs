namespace CoffeeShop_WebApi.QuerybleCustomMethod.ModelOfParameters
{
    public class ProductQueryParameters : QueryParameters
    {
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string? SearchTerm { get; set; }

    }
}
