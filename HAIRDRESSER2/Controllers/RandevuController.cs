using HAIRDRESSER2.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;


namespace HAIRDRESSER2.Controllers
{
    [Authorize] // Bu controller'a yalnızca oturum açmış kullanıcılar erişebilir
    public class RandevuController : Controller
    {
        private readonly ApplicationDbContext db;

        // Dependency Injection ile ApplicationDbContext'i alıyoruz
        public RandevuController(ApplicationDbContext context)
        {
            db = context;
        }


        public IActionResult RandevuAl(int uzmanId, DateTime tarih, TimeSpan saat)
        {
            // Geçerli kullanıcı kimliğini alıyoruz
            var kullaniciId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(kullaniciId))
            {
                return Unauthorized(); // Kullanıcı oturum açmamışsa yetkisiz hata döndür
            }

            // Uzmanı buluyoruz
            var uzman = db.Uzmanlar.Find(uzmanId);
            if (uzman == null)
            {
                return NotFound(); // Uzman bulunamazsa 404 döndür
            }

            // Uzmanın çalışma saatlerini kontrol et
            if (uzman.CalismaSaatleri.Any(s => s.BaslangicSaati <= saat && s.BitisSaati >= saat))
            {
                var randevu = new Randevu
                {
                    UzmanId = uzmanId,
                    Tarih = tarih,
                    Saat = saat,
                    KullaniciId = kullaniciId // KullaniciId'yi string olarak atıyoruz
                };

                db.Randevular.Add(randevu);
                db.SaveChanges();

                return RedirectToAction("Onay", new { id = randevu.Id });
            }

            return View("RandevuAlHata"); // Çalışma saatleri uygun değilse hata sayfası
        }

    }
}
