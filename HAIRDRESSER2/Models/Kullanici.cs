using HAIRDRESSER2.Models;
using Microsoft.AspNetCore.Identity;

namespace HAIRDRESSER2.Models
{
    public class Kullanici 
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string Password { get; set; } // Şifre (hash'lenmiş)
        public string Role { get; set; } // Kullanıcı rolü (örn: "Admin" veya "User")
       // public DateTime CreatedAt { get; set; } // Kullanıcı oluşturulma tarihi

        public virtual List<Randevu> Randevular { get; set; }
    }
}
