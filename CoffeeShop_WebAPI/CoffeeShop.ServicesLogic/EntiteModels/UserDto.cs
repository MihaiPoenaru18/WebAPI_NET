using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.ServicesLogic.EntiteModels
{
    public class UserDto
    {
        
        [EmailAddress]
        public string Email { get; set; } 
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string Role { get; set; }
        [PasswordPropertyText]
        public string Password { get; set; }
        public UserWithNewsLetterDto NewsLetter { get; set; }


    }
}
