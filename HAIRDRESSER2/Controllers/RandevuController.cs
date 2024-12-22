using HAIRDRESSER2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;

namespace HAIRDRESSER2.Controllers
{
    [Authorize]
    public class RandevuController : Controller
    {
        private readonly ApplicationDbContext _db;

        public RandevuController(ApplicationDbContext context)
        {
            _db = context;
        }

        // Randevu alma sayfası (GET)
        [HttpGet]

        [HttpGet]
        [HttpGet]
        public IActionResult RandevuAl(int uzmanId)
        {
            if (uzmanId <= 0)
            {
                // Uzman listesi alınır
                var uzmanlar = _db.Uzmanlar.Include(u => u.UzmanlikAlani).ToList();

                if (!uzmanlar.Any())
                {
                    return Content("Hiç uzman bulunamadı.");
                }

                return View("UzmanSec", uzmanlar);
            }

            var uzman = _db.Uzmanlar
                           .Include(u => u.UzmanlikAlani)
                           .Include(u => u.CalismaSaati)
                           .Include(u => u.Islemler)
                           .FirstOrDefault(u => u.Id == uzmanId);

            if (uzman == null)
            {
                return Content("Uzman bulunamadı: uzmanId = " + uzmanId);
            }

            ViewBag.Uzman = uzman;
            ViewBag.Islemler = uzman.Islemler;
            return View();
        }



        // Randevu alma işlemi (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RandevuAl(int uzmanId, DateTime tarih, TimeSpan saat, int islemId)
        {
            // Kullanıcı kimliğini al
            var kullaniciId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(kullaniciId))
            {
                return Unauthorized("Kullanıcı oturum açmamış.");
            }

            // Uzmanı getir
            var uzman = _db.Uzmanlar
                          .Include(u => u.CalismaSaati)
                          .Include(u => u.Islemler)
                          .FirstOrDefault(u => u.Id == uzmanId);

            if (uzman == null)
            {
                return NotFound("Uzman bulunamadı.");
            }

            // Seçilen işlem kontrolü
            var islem = uzman.Islemler.FirstOrDefault(i => i.Id == islemId);
            if (islem == null)
            {
                ModelState.AddModelError("", "Geçerli bir işlem seçiniz.");
                ViewBag.Uzman = uzman;
                ViewBag.Islemler = uzman.Islemler;
                return View();
            }

            // Randevu zamanı uygun mu?
            var mevcutRandevular = _db.Randevular
                                     .Where(r => r.UzmanId == uzmanId && r.Tarih.Date == tarih.Date)
                                     .ToList();

            if (mevcutRandevular.Any(r => r.Saat <= saat && saat < r.Saat.Add(TimeSpan.FromMinutes(r.Sure))))
            {
                ModelState.AddModelError("", "Seçilen saat dolu. Lütfen başka bir saat seçin.");
                ViewBag.Uzman = uzman;
                ViewBag.Islemler = uzman.Islemler;
                return View();
            }

            // Çalışma saatleri kontrolü
            var calismaSaati = uzman.CalismaSaati;
            if (calismaSaati == null || saat < calismaSaati.BaslangicSaati || saat.Add(TimeSpan.FromMinutes(islem.Sure)) > calismaSaati.BitisSaati)
            {
                ModelState.AddModelError("", "Seçilen saat çalışma saatleri dışında.");
                ViewBag.Uzman = uzman;
                ViewBag.Islemler = uzman.Islemler;
                return View();
            }

            // Yeni randevu oluştur
            var randevu = new Randevu
            {
                UzmanId = uzmanId,
                Tarih = tarih,
                Saat = saat,
                KullaniciId = kullaniciId,
                IslemId = islemId,
                Sure = islem.Sure,
                Ucret = islem.Fiyat
            };

            _db.Randevular.Add(randevu);
            _db.SaveChanges();

            return RedirectToAction("Onay", new { id = randevu.Id });
        }

        // Onay Sayfası
        public IActionResult Onay(int id)
        {
            // Randevuyu ve ilişkili verileri getir
            var randevu = _db.Randevular
                             .Include(r => r.Uzman)
                             .Include(r => r.Islem)
                             .FirstOrDefault(r => r.Id == id);

            if (randevu == null)
            {
                return NotFound("Randevu bulunamadı.");
            }

            return View(randevu);
        }
    }
}
