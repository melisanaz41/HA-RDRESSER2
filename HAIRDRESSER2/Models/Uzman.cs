
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HAIRDRESSER2.Models
{
    public class Uzman
    { // namespace alanı düzenlendi
        public int Id { get; set; }

        [Required(ErrorMessage = "Ad alanı gereklidir.")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Soyad alanı gereklidir.")]
        public string Soyad { get; set; }

        [Required(ErrorMessage = "Telefon alanı gereklidir.")]
        public string Telefon { get; set; }
        //uzman modeli düzeltildi
        [Required(ErrorMessage = "Uzmanlık alanı seçilmelidir.")]

        // [Range(1, int.MaxValue, ErrorMessage = "Uzmanlık alanı seçilmelidir.")]

      
        public int UzmanlikAlaniId { get; set; }

        [ForeignKey("UzmanlikAlaniId")]
        public UzmanlikAlani UzmanlikAlani { get; set; }

        [Required(ErrorMessage = "Çalışma saati seçilmelidir.")]

        //   [Range(1, int.MaxValue, ErrorMessage = "Çalışma saati seçilmelidir.")]
    
        public int CalismaSaatiId { get; set; }
        [ForeignKey("CalismaSaatiId")]
        public CalismaSaati CalismaSaati { get; set; }

        public DateTime EklenmeTarihi { get; set; }
    }
}