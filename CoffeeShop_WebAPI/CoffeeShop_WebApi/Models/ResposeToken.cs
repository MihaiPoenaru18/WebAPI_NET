namespace CoffeeShop_WebApi.Models
{
    public class ResposeToken
    {
        public string Token { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime ExpiresDate { get; set; }
    }
}
