using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HAIRDRESSER2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using System.Linq;

namespace HAIRDRESSER2.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AdminController> _logger;

        public AdminController(
            ApplicationDbContext db,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AdminController> logger)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public IActionResult AdminDashboard() => View();

        public async Task<IActionResult> UzmanListesi()
        {
            var uzmanlar = await _db.Uzmanlar
                                   .Include(u => u.UzmanlikAlani)
                                   .Include(u => u.CalismaSaati)
                                   .ToListAsync();
            return View(uzmanlar);
        }




        [HttpGet]
        public async Task<IActionResult> UzmanEkle()
        {
            var viewModel = await CreateUzmanViewModelAsync();
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
      
        public async Task<IActionResult> UzmanEkle(UzmanViewModel viewModel)
        {
            // Dropdown validasyonu
            if (viewModel.Uzman.UzmanlikAlaniId == 0)
            {
                ModelState.AddModelError("Uzman.UzmanlikAlaniId", "Lütfen geçerli bir uzmanlık alanı seçiniz.");
            }

            if (viewModel.Uzman.CalismaSaatiId == 0)
            {
                ModelState.AddModelError("Uzman.CalismaSaatiId", "Lütfen geçerli bir çalışma saati seçiniz.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("ModelState geçersiz ancak test için işlem devam ediyor.");
            }

            try
            {
                viewModel.Uzman.EklenmeTarihi = DateTime.Now;
                _db.Add(viewModel.Uzman);
                await _db.SaveChangesAsync();
                TempData["SuccessMessage"] = "Uzman başarıyla eklendi.";
                return RedirectToAction("UzmanListesi");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Uzman eklenirken bir hata oluştu.");
                ModelState.AddModelError("", "Bir hata oluştu. Lütfen tekrar deneyin.");
            }

            viewModel = await CreateUzmanViewModelAsync(viewModel.Uzman.UzmanlikAlaniId, viewModel.Uzman.CalismaSaatiId);
            return View(viewModel);
        }



        [HttpGet]
        public async Task<IActionResult> UzmanGuncelle(int id)
        {
            var uzman = await _db.Uzmanlar
                .Include(u => u.CalismaSaati)
                .Include(u => u.UzmanlikAlani)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (uzman == null)
            {
                return NotFound();
            }

            var viewModel = await CreateUzmanViewModelAsync(uzman.UzmanlikAlaniId, uzman.CalismaSaatiId);
            viewModel.Uzman = uzman;
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UzmanGuncelle(UzmanViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _db.Uzmanlar.Update(viewModel.Uzman);
                    await _db.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Uzman başarıyla güncellendi.";
                    return RedirectToAction("UzmanListesi");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Uzman güncellenirken bir hata oluştu.");
                    ModelState.AddModelError("", "Bir hata oluştu. Lütfen tekrar deneyin.");
                }
            }

            viewModel = await CreateUzmanViewModelAsync(viewModel.Uzman.UzmanlikAlaniId, viewModel.Uzman.CalismaSaatiId);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UzmanSil(int id)
        {
            try
            {
                var uzman = await _db.Uzmanlar.FindAsync(id);
                if (uzman != null)
                {
                    _db.Uzmanlar.Remove(uzman);
                    await _db.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Uzman başarıyla silindi.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Uzman silinirken bir hata oluştu.");
                TempData["ErrorMessage"] = "Uzman silinirken bir hata oluştu. Lütfen tekrar deneyin.";
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


        private async Task<UzmanViewModel> CreateUzmanViewModelAsync(int? uzmanlikAlaniId = null, int? calismaSaatiId = null)
        {
            var uzmanlikAlanlari = new List<SelectListItem>
    {
        new SelectListItem { Value = "0", Text = "Seçiniz" }
    };
            uzmanlikAlanlari.AddRange(await _db.UzmanlikAlanlari
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Ad }).ToListAsync());

            var calismaSaatleri = new List<SelectListItem>
    {
        new SelectListItem { Value = "0", Text = "Seçiniz" }
    };
            calismaSaatleri.AddRange(await _db.CalismaSaatleri
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = $"{x.BaslangicSaati} - {x.BitisSaati}"
                }).ToListAsync());

            return new UzmanViewModel
            {
                Uzman = new Uzman(),
                UzmanlikAlanlari = new SelectList(uzmanlikAlanlari, "Value", "Text", uzmanlikAlaniId ?? 0),
                CalismaSaatleri = new SelectList(calismaSaatleri, "Value", "Text", calismaSaatiId ?? 0)
            };
        }



    }
}