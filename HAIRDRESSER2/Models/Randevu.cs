

namespace HAIRDRESSER2.Models
{
    public class Randevu
    {
        public int Id { get; set; }
        public string KullaniciId { get; set; } // Identity ile ilişkilendirme
        public int UzmanId { get; set; } // Uzman ID
        public DateTime Tarih { get; set; } // Randevu Tarihi
        public TimeSpan Saat { get; set; } // Randevu Saati
        public Uzman Uzman { get; set; }
        public ApplicationUser Kullanici { get; set; } // Navigation Property
    }
}
