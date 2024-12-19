using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HAIRDRESSER2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HAIRDRESSER2.Controllers
{
    [Authorize(Roles = "Admin")]
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

        public IActionResult AdminDashboard() => View();

        public IActionResult UzmanListesi()
        {
            var uzmanlar = _db.Uzmanlar
                             .Include(u => u.UzmanlikAlani)
                             .Include(u => u.CalismaSaati)
                             .ToList();
            return View(uzmanlar);
        }

        [HttpGet]
        public IActionResult UzmanEkle()
        {
            FillDropdowns();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public async Task<IActionResult> UzmanEkle([Bind("Ad,Soyad,Telefon,UzmanlikAlaniId,CalismaSaatiId")] Uzman uzman)
        {
            Console.WriteLine($"UzmanlikAlaniId: {uzman.UzmanlikAlaniId}, CalismaSaatiId: {uzman.CalismaSaatiId}");

            if (ModelState.IsValid)
            {
                uzman.EklenmeTarihi = DateTime.Now;
                _db.Add(uzman);
                await _db.SaveChangesAsync();
                return RedirectToAction("UzmanListesi");
            }

            ViewBag.UzmanlikAlanlari = new SelectList(_db.UzmanlikAlanlari, "Id", "Ad", uzman.UzmanlikAlaniId);
            ViewBag.CalismaSaatleri = new SelectList(
                _db.CalismaSaatleri.Select(i => new
                {
                    i.Id,
                    SaatAraligi = $"{i.BaslangicSaati} - {i.BitisSaati}"
                }).ToList(),
                "Id", "SaatAraligi", uzman.CalismaSaatiId);

            return View(uzman);
        }


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

            FillDropdowns(uzman.UzmanlikAlaniId, uzman.CalismaSaatiId);
            return View(uzman);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UzmanGuncelle(Uzman uzman)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _db.Uzmanlar.Update(uzman);
                    _db.SaveChanges();
                    return RedirectToAction("UzmanListesi");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Bir hata oluştu: " + ex.Message);
                }
            }
            FillDropdowns(uzman.UzmanlikAlaniId, uzman.CalismaSaatiId);
            return View(uzman);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UzmanSil(int id)
        {
            try
            {
                var uzman = _db.Uzmanlar.Find(id);
                if (uzman != null)
                {
                    _db.Uzmanlar.Remove(uzman);
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Bir hata oluştu: " + ex.Message);
            }
            return RedirectToAction("UzmanListesi");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Admin");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login() => View();

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

        private void FillDropdowns(int? uzmanlikAlaniId = null, int? calismaSaatiId = null)
        {
            ViewBag.UzmanlikAlanlari = new SelectList(_db.UzmanlikAlanlari, "Id", "Ad", uzmanlikAlaniId);
            ViewBag.CalismaSaatleri = new SelectList(
                _db.CalismaSaatleri.Select(i => new
                {
                    i.Id,
                    SaatAraligi = $"{i.BaslangicSaati} - {i.BitisSaati}"
                }).ToList(),
                "Id", "SaatAraligi", calismaSaatiId);
        }
    }
}
