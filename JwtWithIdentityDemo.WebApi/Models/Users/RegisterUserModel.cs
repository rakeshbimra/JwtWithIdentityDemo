﻿using System.ComponentModel.DataAnnotations;

namespace JwtWithIdentityDemo.WebApi.Models.Users
{
    public class RegisterUserModel
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
