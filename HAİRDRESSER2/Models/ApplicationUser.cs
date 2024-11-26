// Models/ApplicationUser.cs
using Microsoft.AspNetCore.Identity;

namespace HAİRDRESSER2.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Ad { get; set; }       // Kullanıcının adı
        public string Soyad { get; set; }    // Kullanıcının soyadı
    }
}
