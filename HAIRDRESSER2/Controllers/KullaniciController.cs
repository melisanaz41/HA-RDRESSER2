using HAIRDRESSER2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;

namespace HAIRDRESSER2.Controllers
{
    [Authorize(Roles = "User")]
   

    public class KullaniciController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public KullaniciController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Profil()
        {
            var kullaniciId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(kullaniciId))
            {
                return Unauthorized();
            }

            var kullanici = _db.Users
                               .Include(u => u.Randevular) // Kullanıcının randevularını yükle
                               .ThenInclude(r => r.Uzman) // Randevularla birlikte Uzman bilgilerini yükle
                               .FirstOrDefault(u => u.Id == kullaniciId);

            if (kullanici == null)
            {
                return NotFound();
            }

            return View(kullanici);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData["SuccessMessage"] = "Oturum başarıyla kapatıldı.";
            return RedirectToAction("Profil", "Kullanici");
        }



        // Kullanıcının randevularını listeleme
        public IActionResult Randevularim()
        {
            var kullaniciId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(kullaniciId))
            {
                return Unauthorized();
            }

            // İlişkili tabloları yüklemeden, sadece temel sorgu
            var randevular = _db.Randevular
                .Where(r => r.KullaniciId == kullaniciId)
                .Include(r => r.Uzman) // Uzman bilgilerini dahil et
                .Include(r => r.Islem) // İşlem bilgilerini dahil et
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

            var kullanici = _userManager.Users.FirstOrDefault(u => u.Id == kullaniciId);

            if (kullanici == null)
            {
                return NotFound();
            }

            return View(kullanici);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RandevuIptal(int randevuId)
        {
            var randevu = _db.Randevular.FirstOrDefault(r => r.Id == randevuId);
            if (randevu == null)
            {
                return NotFound("Randevu bulunamadı.");
            }

            try
            {
                _db.Randevular.Remove(randevu);
                _db.SaveChanges();
                TempData["SuccessMessage"] = "Randevu başarıyla iptal edildi.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Randevu iptal edilirken bir hata oluştu.";
               // _logger.LogError(ex, "Randevu iptal edilirken bir hata oluştu.");
            }

            return RedirectToAction("Randevularim", "Kullanici");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RandevuIptalConfirmed(int id)
        {
            var randevu = _db.Randevular.FirstOrDefault(r => r.Id == id);

            if (randevu == null)
            {
                TempData["ErrorMessage"] = "Randevu bulunamadı.";
                return RedirectToAction("Randevularim");
            }

            _db.Randevular.Remove(randevu);
            _db.SaveChanges();
            TempData["SuccessMessage"] = "Randevu başarıyla iptal edildi.";

            return RedirectToAction("Randevularim");
        }




        // Kullanıcı profilini düzenleme işlemi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProfilDuzenle(ApplicationUser model)
        {
            var kullaniciId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(kullaniciId))
            {
                return Unauthorized();
            }

            var kullanici = await _userManager.FindByIdAsync(kullaniciId);

            if (kullanici == null)
            {
                return NotFound();
            }

            // Kullanıcı bilgilerini güncelle
            kullanici.Ad = model.Ad;
            kullanici.Soyad = model.Soyad;
            kullanici.Email = model.Email;
            kullanici.PhoneNumber = model.PhoneNumber;

            var result = await _userManager.UpdateAsync(kullanici);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }

            return RedirectToAction("Profil");
        }
   


        //deneme
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


