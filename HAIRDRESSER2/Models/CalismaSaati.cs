namespace HAIRDRESSER2.Models
{
    public class CalismaSaati
    {
        public int Id { get; set; }
        // public DayOfWeek Gun { get; set; }

        //public string Gun { get; set; } = string.Empty; // Örnek
        public TimeSpan BaslangicSaati { get; set; } // Örnek
        public TimeSpan BitisSaati { get; set; } // Örnek
                                                 //    public Uzman Uzman { get; set; } // Uzman ile ilişki
        public virtual ICollection<Uzman> Uzmanlar { get; set; }
    }

}