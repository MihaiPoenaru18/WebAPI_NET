
namespace CoffeeShop.DataAccess.DataAccess.ModelDB
{
    public class UserWithNewsLetter
    {
        
        public Guid Id { get; set; }
        public string Email { get; set; }= string.Empty;
        public bool IsNewsLetterActive { get; set; }
    }
}
