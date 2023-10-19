
using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.ServicesLogic.EntiteModels
{
    public class UserWithNewsLetterDto
    {
        [EmailAddress]
        public string Email { get; set; }
        public bool IsActived { get; set; }
    }
}
