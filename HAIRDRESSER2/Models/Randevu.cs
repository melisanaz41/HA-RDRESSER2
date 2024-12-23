

using System.ComponentModel.DataAnnotations;

namespace HAIRDRESSER2.Models
{
    public class Randevu
    {
        public int Id { get; set; }
        public string KullaniciId { get; set; } // Identity ile ilişkilendirme
        public int UzmanId { get; set; } // Uzman ID
        public int IslemId { get; set; } // İşlem ID
        public DateTime Tarih { get; set; } // Randevu Tarihi
        public TimeSpan Saat { get; set; } // Randevu Saati
        public int Sure { get; set; } // İşlemin süresi (dakika)
        public decimal Ucret { get; set; } // İşlemin ücreti

        // Navigation Properties
        public Uzman Uzman { get; set; }
        public ApplicationUser Kullanici { get; set; } // Navigation Property
        public Islem Islem { get; set; } // İşlem tablosu ile ilişki
    }
}
