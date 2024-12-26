namespace HAIRDRESSER2.Models
{
    public class RandevuDurumu
    {

        public int Id { get; set; }
        public int RandevuId { get; set; } // İlgili randevu
        public string Durum { get; set; } // "Gelindi", "Gelinmedi", "İptal Edildi", vb.
        public DateTime IslemTarihi { get; set; } // Durumun güncellendiği tarih

        public Randevu Randevu { get; set; } // Navigation Property


    }
}
