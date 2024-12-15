namespace HAIRDRESSER2.Models
{
    public class Randevu
    {
        public int Id { get; set; }
        public int KullaniciId { get; set; }
        public virtual Kullanici Kullanici { get; set; }
        public int UzmanId { get; set; }
        public virtual Uzman Uzman { get; set; }
        public DateTime Tarih { get; set; }
        public TimeSpan Saat { get; set; }
        public DayOfWeek Gun { get; set; }
    }
}
