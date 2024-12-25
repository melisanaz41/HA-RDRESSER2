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

        public KullaniciController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
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


        // Kullanıcının randevularını listeleme
        public IActionResult Randevularim()
        {
            var kullaniciId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(kullaniciId))
            {
                return Unauthorized();
            }

            var randevular = _db.Randevular
                .Where(r => r.KullaniciId == kullaniciId)
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

