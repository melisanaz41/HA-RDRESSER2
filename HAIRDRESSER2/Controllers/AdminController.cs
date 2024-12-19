using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HAIRDRESSER2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace HAIRDRESSER2.Controllers
{
    [Authorize(Roles = "Admin")] // Sadece Admin rolü olanlar erişebilir
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AdminController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Admin Dashboard
        public IActionResult AdminDashboard()
        {
            return View();
        }

        // Uzman Listesi
        public IActionResult UzmanListesi()
        {
            var uzmanlar = _db.Uzmanlar
                            .Include(u => u.UzmanlikAlani)
                            .Include(u => u.CalismaSaati)
                            .ToList();
            return View(uzmanlar);
        }

        //Uzman Ekleme  (GET)
        [HttpGet]
        public IActionResult UzmanEkle()
        {
            ViewBag.UzmanlikAlanlari = _db.UzmanlikAlanlari.ToList();
            ViewBag.CalismaSaatleri = _db.CalismaSaati
                .Select(cs => new
                {
                    Id = cs.Id,
                    SaatAraligi = $"{cs.BaslangicSaati} - {cs.BitisSaati}"
                }).ToList();

            return View();
        }

        // Yeni Uzman Ekleme (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UzmanEkle(Uzman uzman)
        {
            if (ModelState.IsValid)
            {
                uzman.EklenmeTarihi = DateTime.Now; // Eklenme tarihini otomatik ata
                _db.Uzmanlar.Add(uzman);
                _db.SaveChanges();
                return RedirectToAction("UzmanListesi");
            }

            ViewBag.UzmanlikAlanlari = _db.UzmanlikAlanlari.ToList();
            ViewBag.CalismaSaatleri = _db.CalismaSaati.ToList();
            return View(uzman);
        }

        // Uzman Güncelleme (GET)
        [HttpGet]
        public IActionResult UzmanGuncelle(int id)
        {
            var uzman = _db.Uzmanlar
                .Include(u => u.CalismaSaati)
                .FirstOrDefault(u => u.Id == id);

            if (uzman == null)
            {
                return NotFound();
            }

            ViewBag.UzmanlikAlanlari = _db.UzmanlikAlanlari.ToList();
            ViewBag.CalismaSaatleri = _db.CalismaSaati.ToList();
            return View(uzman);
        }

        // Uzman Güncelleme (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UzmanGuncelle(Uzman uzman)
        {
            if (ModelState.IsValid)
            {
                _db.Uzmanlar.Update(uzman);
                _db.SaveChanges();
                return RedirectToAction("UzmanListesi");
            }

            ViewBag.UzmanlikAlanlari = _db.UzmanlikAlanlari.ToList();
            ViewBag.CalismaSaatleri = _db.CalismaSaati.ToList();
            return View(uzman);
        }

        // Uzman Silme
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UzmanSil(int id)
        {
            var uzman = _db.Uzmanlar.Find(id);
            if (uzman != null)
            {
                _db.Uzmanlar.Remove(uzman);
                _db.SaveChanges();
            }

            return RedirectToAction("UzmanListesi");
        }

        // Admin Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Admin");
        }

        // Admin Login (GET)
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Admin Login (POST)
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    if (await _userManager.IsInRoleAsync(user, "Admin"))
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("AdminDashboard");
                    }

                    ModelState.AddModelError("", "Admin yetkisi bulunmamaktadır.");
                }
                else
                {
                    ModelState.AddModelError("", "Geçersiz giriş bilgileri.");
                }
            }
            return View(model);
        }
    }
}
