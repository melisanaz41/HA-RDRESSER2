namespace HAIRDRESSER2.Models
{
    public class RandevuViewModel
    {
        public DateTime Tarih { get; set; }
        public TimeSpan Saat { get; set; }
        public int IslemId { get; set; }
        public int UzmanId { get; set; }
        //randevuvieewwa
        public IEnumerable<Islem> Islemler { get; set; }
        public IEnumerable<Uzman> Uzmanlar { get; set; }
    }
}
