using CoffeeShop.DataAccess.DataAccess.ModelDB.OrderModels;
using System.Text.Json.Serialization;

namespace CoffeeShop.DataAccess.DataAccess.ModelDB.UserModels
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Guid IdUserNewsLetter { get; set; }
        public UserWithNewsLetter UserWithNewsLetter { get; set; }

        [JsonIgnore]
        public List<Order>Orders { get; set; }
    }
}
