
namespace CoffeeShop.DataAccess.DataAccess.ModelDB
{
    public class UserWithNewsLetter
    {
        
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsNewsLetterActive { get; set; }
    }
}
