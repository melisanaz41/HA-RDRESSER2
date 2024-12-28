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
            Console.WriteLine($"[DEBUG] RandevuAl - UzmanId: {uzmanId}");

            if (uzmanId <= 0)
            {
                var uzmanlar = db.Uzmanlar.Include(u => u.UzmanlikAlani).ToList();
                if (!uzmanlar.Any())
                {
                    return Content("Hiç uzman bulunamadı.");
                }
                return View("UzmanSec", uzmanlar);
            }

            var uzman = db.Uzmanlar
                          .Include(u => u.UzmanlikAlani)
                          .Include(u => u.CalismaSaati)
                          .Include(u => u.Islemler)
                          .FirstOrDefault(u => u.Id == uzmanId);

            if (uzman == null)
            {
                return Content($"Uzman bulunamadı: uzmanId = {uzmanId}");
            }

            ViewBag.SelectedUzman = uzman;
            ViewBag.Islemler = uzman.Islemler;

            return View();
        }


        // Randevu alma işlemi (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitRandevu(int uzmanId, DateTime tarih, TimeSpan saat, int islemId)
        {
            Console.WriteLine($"[DEBUG] SubmitRandevu - UzmanId: {uzmanId}, Tarih: {tarih}, Saat: {saat}, IslemId: {islemId}");

            if (uzmanId <= 0)
            {
                Console.WriteLine("[ERROR] UzmanId sıfır veya geçersiz.");
                ModelState.AddModelError("", "Geçersiz uzman seçimi.");
                return View("RandevuAl");
            }

            if (tarih.Date < DateTime.Now.Date)
            {
                Console.WriteLine("[ERROR] Geçmiş bir tarih seçildi.");
                ModelState.AddModelError("", "Seçim yapmak istediğiniz tarih güncel değildir, tekrar deneyiniz.");
                return View("RandevuAl");
            }

            var kullaniciId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(kullaniciId))
            {
                Console.WriteLine("[ERROR] Kullanıcı kimliği bulunamadı.");
                return Unauthorized("Kullanıcı oturum açmamış.");
            }

            var uzman = db.Uzmanlar
                          .Include(u => u.UzmanlikAlani)
                          .Include(u => u.CalismaSaati)
                          .Include(u => u.Islemler)
                          .FirstOrDefault(u => u.Id == uzmanId);

            if (uzman == null)
            {
                Console.WriteLine($"[ERROR] Geçersiz UzmanId: {uzmanId}");
                ModelState.AddModelError("", "Geçersiz uzman seçimi.");
                return View("RandevuAl");
            }

            var calismaSaati = uzman.CalismaSaati;
            if (calismaSaati == null || saat < calismaSaati.BaslangicSaati || saat >= calismaSaati.BitisSaati)
            {
                var calismaSaatMesaji = $"Seçtiğiniz randevu saati uzman çalışma saatleri dışındadır. Lütfen {calismaSaati.BaslangicSaati:hh\\:mm} - {calismaSaati.BitisSaati:hh\\:mm} saatleri arasında bir seçim yapınız.";
                Console.WriteLine($"[WARNING] Çalışma saati kontrolü hatalı: {calismaSaatMesaji}");

                ModelState.AddModelError("", calismaSaatMesaji);
                ViewBag.SelectedUzman = uzman;
                ViewBag.Islemler = uzman.Islemler;
                return View("RandevuAl");
            }

            // Uzmanın mevcut randevularını kontrol et
            var randevular = db.Randevular
                .Where(r => r.UzmanId == uzmanId && r.Tarih == tarih)
                .ToList();

            foreach (var randevu in randevular)
            {
                var randevuBaslangic = randevu.Saat;
                var randevuBitis = randevu.Saat + TimeSpan.FromMinutes(randevu.Islem.Sure);

                if (saat >= randevuBaslangic && saat < randevuBitis)
                {
                    var randevuSaatMesaji = $"Seçmek istediğiniz saatler aralığında uzmanımızın mevcut bir randevusu vardır. Lütfen {calismaSaati.BaslangicSaati:hh\\:mm} - {calismaSaati.BitisSaati:hh\\:mm} saatleri arasında başka bir saat seçiniz.";
                    Console.WriteLine($"[WARNING] Mevcut randevu çakışması: {randevuSaatMesaji}");

                    ModelState.AddModelError("", randevuSaatMesaji);
                    ViewBag.SelectedUzman = uzman;
                    ViewBag.Islemler = uzman.Islemler;
                    return View("RandevuAl");
                }
            }

            var yeniRandevu = new Randevu
            {
                UzmanId = uzmanId,
                Tarih = tarih,
                Saat = saat,
                KullaniciId = kullaniciId,
                IslemId = islemId
            };

            db.Randevular.Add(yeniRandevu);

            try
            {
                db.SaveChanges();
                Console.WriteLine("[SUCCESS] Randevu başarıyla kaydedildi.");
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"[ERROR] Veritabanı hatası: {ex.InnerException?.Message}");
                ModelState.AddModelError("", "Randevu kaydedilirken bir hata oluştu.");
                ViewBag.SelectedUzman = uzman;
                ViewBag.Islemler = uzman.Islemler;
                return View("RandevuAl");
            }

            return RedirectToAction("Onay", new { id = yeniRandevu.Id });
        }


        // Onay Sayfası
        public IActionResult Onay(int id)
        {
            var randevu = db.Randevular
                             .Include(r => r.Uzman)
                             .FirstOrDefault(r => r.Id == id);

            if (randevu == null)
            {
                return NotFound("Randevu bulunamadı.");
            }

            return View(randevu);
        }
    


    }
}
