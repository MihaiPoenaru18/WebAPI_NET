﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public  string PasswordHash { get; set; } = string.Empty;
    }
}
