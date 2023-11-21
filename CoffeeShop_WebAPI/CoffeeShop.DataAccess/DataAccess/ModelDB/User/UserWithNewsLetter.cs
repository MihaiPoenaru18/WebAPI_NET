using System.Text.Json.Serialization;

namespace CoffeeShop.DataAccess.DataAccess.ModelDB.User
{
    public class UserWithNewsLetter
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsNewsLetterActive { get; set; }
        [JsonIgnore]
        public User User { get; set; }
    }
}
