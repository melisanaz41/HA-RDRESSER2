namespace HAİRDRESSER2.Models
{
    public class CalismaSaati
    {
         public int Id { get; set; }
    public int UzmanId { get; set; }
    public virtual Uzman Uzman { get; set; }
    public DayOfWeek Gun { get; set; }
    public TimeSpan BaslangicSaati { get; set; }
    public TimeSpan BitisSaati { get; set; }
    }
}
