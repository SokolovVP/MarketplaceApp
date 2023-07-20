using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceApp.Models
{
    public class User
    {
        [Key]
        public int userId { get; set; }
        [UsernameValidator]
        public string userName { get; set; }
        [Required (ErrorMessage = "name is the required field")]
        public string name { get; set; }
        [Required (ErrorMessage = "surname is the required field")]
        public string surname { get; set; }
        [EmailValidator]
        public string email { get; set; }
        public DateTime registrationDate { get; set; } = DateTime.Today;
        public string category { get; set; } 
        [LoginValidator]
        public string login { get; set; }
        [Required (ErrorMessage = "password is the required field")]
        [RegularExpression(@"[A-Za-z0-9]+[\.\#\%\$\\\^\&\*\:\|\;|,\/\-\+\!\?\@\№]")]
        public string password { get; set; }
        public override string ToString() => JsonSerializer.Serialize<User>(this);
    }
}
