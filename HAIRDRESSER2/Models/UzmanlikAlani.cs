using System.Collections.Generic;

namespace HAIRDRESSER2.Models
{
    public class UzmanlikAlani
    {
        public int Id { get; set; }
        public string Ad { get; set; } // Uzmanlık alanı adı (örn: Saç, Makyaj)

        // İlişki: Bir uzmanlık alanına birden fazla uzman bağlanabilir
        public virtual ICollection<Uzman> Uzmanlar { get; set; }
    }
}
