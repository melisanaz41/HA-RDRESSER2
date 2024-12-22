namespace HAIRDRESSER2.Models
{
    public class Islem
    {

        public int Id { get; set; } // İşlem ID
        public string Ad { get; set; } // İşlem adı
        public decimal Fiyat { get; set; } // İşlem fiyatı
        public int UzmanlikAlaniId { get; set; } // Uzmanlık alanı ID
        public UzmanlikAlani UzmanlikAlani { get; set; } // Uzmanlık alanı
        public int Sure { get; set; } // İşlem süresi 


    }
}
