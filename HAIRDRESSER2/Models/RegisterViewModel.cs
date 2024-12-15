// Models/RegisterViewModel.cs
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HAIRDRESSER2.Models
{
    public class RegisterViewModel: IdentityUser
    {
        [Required]
        [Display(Name = "Ad")]
        public string Ad { get; set; }

        [Required]
        [Display(Name = "Soyad")]
        public string Soyad { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Parola")]
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
