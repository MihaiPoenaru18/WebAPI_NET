﻿using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.DataAccess.DataAccess.ModelDB.User
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Guid IdUserNewsLetter { get; set; }

        [ForeignKey("IdUserNewsLetter")]
        public UserWithNewsLetter UserWithNewsLetter { get; set; }
    }
}