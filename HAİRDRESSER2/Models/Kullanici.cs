namespace HAİRDRESSER2.Models
{
    public class Kullanici
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public virtual List<Randevu> Randevular { get; set; }
    }
}
