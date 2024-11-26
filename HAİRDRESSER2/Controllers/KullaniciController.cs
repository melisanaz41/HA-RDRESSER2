using HAİRDRESSER2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace HAİRDRESSER2.Controllers
{
    [Authorize] // Sadece oturum açmış kullanıcıların erişebileceğini belirtir
    public class KullaniciController : Controller
    {
        private readonly ApplicationDbContext _db;

        // Constructor Injection kullanarak ApplicationDbContext'i alıyoruz
        public KullaniciController(ApplicationDbContext db)
        {
            _db = db;
        }

        // Kullanıcı profilini görüntüleme
        public IActionResult Profil()
        {
            // Şu anki oturum açmış kullanıcının kimliğini alıyoruz
            var kullaniciId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(kullaniciId))
            {
                return Unauthorized(); // Kullanıcı oturum açmamışsa yetkisiz hata döndür
            }

            // Kullanıcı bilgilerini veritabanından alıyoruz
            var kullanici = _db.Kullanicilar.FirstOrDefault(k => k.Id == int.Parse(kullaniciId));

            if (kullanici == null)
            {
                return NotFound(); // Kullanıcı bulunamazsa 404 döndür
            }

            return View(kullanici); // Profil bilgilerini döndür
        }

        // Kullanıcının randevularını listeleme
        public IActionResult Randevularim()
        {
            // Şu anki oturum açmış kullanıcının kimliğini alıyoruz
            var kullaniciId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(kullaniciId))
            {
                return Unauthorized(); // Kullanıcı oturum açmamışsa yetkisiz hata döndür
            }

            // Kullanıcının randevularını alıyoruz
            var randevular = _db.Randevular
                                .Where(r => r.KullaniciId == int.Parse(kullaniciId))
                                .ToList();

            return View(randevular);
        }

        // Kullanıcı profilini düzenleme sayfası
        [HttpGet]
        public IActionResult ProfilDuzenle()
        {
            var kullaniciId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(kullaniciId))
            {
                return Unauthorized();
            }

            var kullanici = _db.Kullanicilar.FirstOrDefault(k => k.Id == int.Parse(kullaniciId));

            if (kullanici == null)
            {
                return NotFound();
            }

            return View(kullanici);
        }

        // Kullanıcı profilini düzenleme işlemi
        [HttpPost]
        public IActionResult ProfilDuzenle(Kullanici model)
        {
            var kullaniciId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(kullaniciId))
            {
                return Unauthorized();
            }

            var kullanici = _db.Kullanicilar.FirstOrDefault(k => k.Id == int.Parse(kullaniciId));

            if (kullanici == null)
            {
                return NotFound();
            }

            // Kullanıcı bilgilerini güncelleme
            kullanici.Ad = model.Ad;
            kullanici.Soyad = model.Soyad;
            kullanici.Email = model.Email;
            kullanici.Telefon = model.Telefon;

            _db.SaveChanges(); // Veritabanında değişiklikleri kaydet

            return RedirectToAction("Profil"); // Güncelleme sonrası profil sayfasına yönlendir
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
