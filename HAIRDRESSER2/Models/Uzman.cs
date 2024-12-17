namespace HAIRDRESSER2.Models
{
    public class Uzman
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public int UzmanlikAlaniId { get; set; }
        public UzmanlikAlani UzmanlikAlani { get; set; }
        public string Telefon { get; set; }
        public DateTime EklenmeTarihi { get; set; }
       public int CalismaSaatiId { get; set; }
        public CalismaSaati CalismaSaati{ get; set; }
        
       // public virtual List<Randevu> Randevular { get; set; }
    }
}
