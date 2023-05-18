using CoffeeShop_WebApi.Models;

namespace CoffeeShop_WebApi.Authorization.Models
{
    public class AuthenticateResponse
    {
        public AuthenticateResponse(AuthenticateRequest request, string token)
        {
            Email = request.Email;
            Token = token;
        }
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime ExpiresDate { get; set; }

        
    }
}
