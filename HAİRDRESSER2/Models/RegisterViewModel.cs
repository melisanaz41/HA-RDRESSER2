// Models/RegisterViewModel.cs
using System.ComponentModel.DataAnnotations;

namespace HAİRDRESSER2.Models
{
    public class RegisterViewModel
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

        [DataType(DataType.Password)]
        [Display(Name = "Parola Onay")]
        [Compare("Password", ErrorMessage = "Parolalar eşleşmiyor.")]
        public string ConfirmPassword { get; set; }
    }
}
