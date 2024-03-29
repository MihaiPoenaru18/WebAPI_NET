﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.ServicesLogic.EntiteModels
{
    public class UserDto
    {
        
        [EmailAddress]
        public required string Email { get; set; } 
        public required string FirstName { get; set; } 
        public required string LastName { get; set; } 
        public required string Role { get; set; }
        [PasswordPropertyText]
        public required string Password { get; set; }
        public UserWithNewsLetterDto NewsLetter { get; set; }


    }
}
