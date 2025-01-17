﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace User.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        //public string Author { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        //public string Provider { get; set; }

        //public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
