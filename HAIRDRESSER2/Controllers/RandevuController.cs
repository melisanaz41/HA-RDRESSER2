using HAIRDRESSER2.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

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

        // Randevu alma sayfası (GET)
        [HttpGet]
        public IActionResult RandevuAl(int uzmanId)
        {
            var uzman = db.Uzmanlar
                         .Include(u => u.CalismaSaati)
                         .FirstOrDefault(u => u.Id == uzmanId);

            if (uzman == null)
            {
                return NotFound(); // Uzman bulunamazsa 404 döndür
            }

            ViewBag.Uzman = uzman;
            return View();
        }

        // Randevu alma işlemi (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RandevuAl(int uzmanId, DateTime tarih, TimeSpan saat)
        {
            // Geçerli kullanıcı kimliğini alıyoruz
            var kullaniciId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(kullaniciId))
            {
                return Unauthorized(); // Kullanıcı oturum açmamışsa yetkisiz hata döndür
            }

            // Uzmanı ve çalışma saatlerini dahil ederek buluyoruz
            var uzman = db.Uzmanlar
                         .Include(u => u.CalismaSaati)
                         .FirstOrDefault(u => u.Id == uzmanId);

            if (uzman == null)
            {
                return NotFound(); // Uzman bulunamazsa 404 döndür
            }

            // Uzmanın çalışma saatlerini kontrol et
            var calismaSaati = uzman.CalismaSaati;
            if (calismaSaati != null &&
                calismaSaati.BaslangicSaati <= saat &&
                calismaSaati.BitisSaati >= saat)
            {
                var randevu = new Randevu
                {
                    UzmanId = uzmanId,
                    Tarih = tarih,
                    Saat = saat,
                    KullaniciId = kullaniciId // Kullanıcı kimliğini ata
                };

                db.Randevular.Add(randevu);
                db.SaveChanges();

                return RedirectToAction("Onay", new { id = randevu.Id }); // Onay sayfasına yönlendir
            }

            // Çalışma saati uygun değilse hata mesajı döndür
            ModelState.AddModelError("", "Seçilen saat çalışma saatleri dışında!");
            ViewBag.Uzman = uzman;
            return View();
        }

        // Onay Sayfası
        public IActionResult Onay(int id)
        {
            var randevu = db.Randevular
                            .Include(r => r.Uzman)
                            .FirstOrDefault(r => r.Id == id);

            if (randevu == null)
            {
                return NotFound();
            }

            return View(randevu);
        }
    }
}
