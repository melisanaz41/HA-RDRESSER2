using Microsoft.AspNetCore.Identity;

namespace HAIRDRESSER2.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Ad ve Soyad gibi ek alanlar
        public string Ad { get; set; }
        public string Soyad { get; set; }
     //   public string ProfilePhotoPath { get; set; }
        public virtual ICollection<Randevu> Randevular { get; set; }
        // Diğer özelleştirilmiş özellikler
    }
}
