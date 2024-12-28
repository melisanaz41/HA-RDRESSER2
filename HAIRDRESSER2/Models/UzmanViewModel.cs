
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HAIRDRESSER2.Models
{
    public class UzmanViewModel
    {
        public Uzman Uzman { get; set; }
        public SelectList UzmanlikAlanlari { get; set; }
        public SelectList CalismaSaatleri { get; set; }

        public List<Randevu> Randevular { get; set; } // İlişkili randevular
    }
}