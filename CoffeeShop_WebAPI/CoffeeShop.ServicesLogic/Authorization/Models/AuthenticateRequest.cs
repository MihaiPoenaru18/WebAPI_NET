using System.Globalization;

namespace CoffeeShop_WebApi.Authorization.Models
{
    public class AuthenticateRequest
    {
        public required string Email { get; set; }
        public string Role { get; set; } = "User";
        public required string Password { get; set; }
    }
}
