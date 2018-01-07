using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace IdentityTask.Models
{
    public class User :IdentityUser
    {
        public string Country { get; set; } = "Ukraine";
        [Required]
        [Range(1, 150)]
        public int Age { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Range(1, 150)]
        public int Age { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class UserInfoModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string Country { get; set; }
    }
}
