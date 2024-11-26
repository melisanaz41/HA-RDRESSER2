namespace HAİRDRESSER2.Models
{
    public class Uzman
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Branş { get; set; } // Örneğin Saç veya Tırnak
        public virtual List<CalismaSaati> CalismaSaatleri { get; set; }
        public virtual List<Randevu> Randevular { get; set; }
    }
}
