namespace CoffeeShop_WebApi.Models
{
    public class LoginUser
    {
        public required string Email { get; set; }
        public string Role { get; set; } = "User";
        public required string Password { get; set; }
    }
}
