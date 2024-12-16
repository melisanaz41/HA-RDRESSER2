using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HAIRDRESSER2.Models;
using Microsoft.AspNetCore.Authorization;

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
            return View(); // Admin Dashboard görünümü
        }

        // Admin Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Admin");
        }

        // Uzman Listesi
        public IActionResult UzmanListesi()
        {
            var uzmanlar = _db.Uzmanlar.ToList();
            return View(uzmanlar);
        }

        // Yeni Uzman Ekleme (GET)
        [HttpGet]
        public IActionResult UzmanEkle()
        {
            ViewBag.UzmanlikAlanlari = _db.UzmanlikAlanlari.ToList(); // Uzmanlık alanlarını ViewBag'e ekle
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UzmanEkle(Uzman uzman)
        {
            if (ModelState.IsValid)
            {
                uzman.EklenmeTarihi = DateTime.Now;
                _db.Uzmanlar.Add(uzman);
                _db.SaveChanges();
                return RedirectToAction("UzmanListesi");
            }
            ViewBag.UzmanlikAlanlari = _db.UzmanlikAlanlari.ToList(); // Hata durumunda tekrar listeyi yükle
            return View(uzman);
        }

        // Uzman Güncelleme (GET)
        [HttpGet]
        public IActionResult UzmanGuncelle(int id)
        {
            var uzman = _db.Uzmanlar.FirstOrDefault(u => u.Id == id);
            if (uzman == null)
            {
                return NotFound();
            }
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
            return View(uzman);
        }

        // Uzman Silme
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UzmanSil(int id)
        {
            var uzman = _db.Uzmanlar.FirstOrDefault(u => u.Id == id);
            if (uzman != null)
            {
                _db.Uzmanlar.Remove(uzman);
                _db.SaveChanges();
            }
            return RedirectToAction("UzmanListesi");
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
                        return RedirectToAction("AdminDashboard", "Admin");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Admin girişine izin verilmemektedir.");
                    }
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
