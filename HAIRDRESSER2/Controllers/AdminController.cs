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
        [HttpGet]
        public IActionResult Randevular()
        {
            var randevular = _db.Randevular
                                .Include(r => r.Uzman)
                                .Include(r => r.Islem)
                                .ToList();

            var randevuDurumlari = _db.RandevuDurumlari.ToList();

            var viewModel = randevular.Select(r => new
            {
                Randevu = r,
                Durum = randevuDurumlari.FirstOrDefault(d => d.RandevuId == r.Id)?.Durum
            }).ToList();

            return View(viewModel);
        }
        [HttpGet]
        public IActionResult Raporlama()
        {
            var gelinenRandevular = _db.RandevuDurumlari
                .Where(d => d.Durum == "Gelindi")
                .Include(d => d.Randevu)
                .ThenInclude(r => r.Islem)
                .Include(d => d.Randevu.Uzman)
                .ToList();

            var toplamKazanc = gelinenRandevular.Sum(r => r.Randevu.Islem.Fiyat);
            var toplamGelinenRandevu = gelinenRandevular.Count;

            var uzmanRaporlari = gelinenRandevular
                .GroupBy(r => r.Randevu.Uzman)
                .Select(g => new
                {
                    Uzman = g.Key,
                    RandevuSayisi = g.Count(),
                    ToplamKazanc = g.Sum(r => r.Randevu.Islem.Fiyat)
                }).ToList();

            var rapor = new
            {
                ToplamKazanc = toplamKazanc,
                ToplamGelinenRandevu = toplamGelinenRandevu,
                UzmanRaporlari = uzmanRaporlari
            };

            return View(rapor);
        }

        // Randevu durumu güncelleme
        [HttpPost]
        public IActionResult RandevuDurumuGuncelle(int randevuId, string durum)
        {
            var mevcutDurum = _db.RandevuDurumlari.FirstOrDefault(d => d.RandevuId == randevuId);

            if (mevcutDurum == null)
            {
                // Yeni durum ekle
                var yeniDurum = new RandevuDurumu
                {
                    RandevuId = randevuId,
                    Durum = durum,
                    IslemTarihi = DateTime.Now
                };
                _db.RandevuDurumlari.Add(yeniDurum);
            }
            else
            {
                // Mevcut durumu güncelle
                mevcutDurum.Durum = durum;
                mevcutDurum.IslemTarihi = DateTime.Now;
                _db.RandevuDurumlari.Update(mevcutDurum);
            }

            _db.SaveChanges();
            return RedirectToAction("Randevular");
        }

        // Randevu silme işlemi
    





[HttpGet]
public async Task<IActionResult> GetIslemlerByUzmanlikAlani(int id)
{
    var islemler = await _db.Islemler
                           .Where(i => i.UzmanlikAlaniId == id)
                           .Select(i => new { i.Ad, i.Fiyat, i.Sure })
                           .ToListAsync();
    return Json(islemler);
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

                // Uzmanlık alanına ait işlemleri ekle
                var islemler = await _db.Islemler
                                        .Where(i => i.UzmanlikAlaniId == viewModel.Uzman.UzmanlikAlaniId)
                                        .ToListAsync();

                viewModel.Uzman.Islemler = islemler;

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

            return View(viewModel); // UzmanViewModel gönderiliyor
        }


        [HttpPut("api/uzmanlar/{id}")]
        public async Task<IActionResult> UpdateUzman(int id, [FromBody] Uzman updatedUzman)
        {
            // ID doğrulama
            if (id != updatedUzman.Id)
            {
                return BadRequest(new { message = "Uzman ID uyuşmuyor." });
            }

            // Mevcut uzmanı bulma
            var mevcutUzman = await _db.Uzmanlar.FirstOrDefaultAsync(u => u.Id == id);
            if (mevcutUzman == null)
            {
                return NotFound(new { message = "Uzman bulunamadı." });
            }

            // Güncelleme işlemi
            mevcutUzman.Ad = updatedUzman.Ad;
            mevcutUzman.Soyad = updatedUzman.Soyad;
            mevcutUzman.UzmanlikAlaniId = updatedUzman.UzmanlikAlaniId;
            mevcutUzman.Telefon = updatedUzman.Telefon;

            try
            {
                // Veritabanında değişiklikleri kaydet
                _db.Uzmanlar.Update(mevcutUzman); // Güncellenen kaydı işaretlemek için Update çağrısı
                await _db.SaveChangesAsync();

                return Ok(new { message = "Uzman başarıyla güncellendi.", uzman = mevcutUzman });
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(500, new { message = "Veritabanı güncelleme hatası.", error = dbEx.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Bir hata oluştu.", error = ex.Message });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UzmanSil(int id)
        {
            try
            {
                // İlgili uzmanı bulun
                var uzman = await _db.Uzmanlar.FirstOrDefaultAsync(u => u.Id == id);

                if (uzman == null)
                {
                    TempData["ErrorMessage"] = "Uzman bulunamadı.";
                    return RedirectToAction("UzmanListesi");
                }

                // Uzman ile ilişkili randevuları bulun
                var randevular = await _db.Randevular
                                          .Where(r => r.UzmanId == id)
                                          .ToListAsync();

                // Uzman ile ilişkili randevuları sil
                if (randevular.Any())
                {
                    _db.Randevular.RemoveRange(randevular);
                }

                // Uzmanı sil
                _db.Uzmanlar.Remove(uzman);

                // Veritabanına değişiklikleri kaydet
                await _db.SaveChangesAsync();

                TempData["SuccessMessage"] = "Uzman ve ilişkili randevular başarıyla silindi.";
            }
            catch (Exception ex)
            {
                // Hata durumunda loglama ve mesaj
                _logger.LogError(ex, "Uzman silinirken bir hata oluştu.");
                TempData["ErrorMessage"] = "Uzman silinirken bir hata oluştu. Lütfen tekrar deneyin.";
            }

            return RedirectToAction("UzmanListesi");
        }

        public async Task<IActionResult> UzmanDetay(int id)
        {
            var uzman = await _db.Uzmanlar
                .Include(u => u.UzmanlikAlani) // Uzmanlık alanını dahil et
                .FirstOrDefaultAsync(u => u.Id == id);

            if (uzman == null)
            {
                return NotFound("Uzman bulunamadı.");
            }

            var randevular = await _db.Randevular
                .Where(r => r.UzmanId == id)
                .ToListAsync();

            var viewModel = new UzmanViewModel
            {
                Uzman = uzman,
                Randevular = randevular
            };

            return View(viewModel);
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
        [HttpGet]
        public IActionResult GelinmeyenRandevular()
        {
            var gelinmeyenRandevular = _db.RandevuDurumlari
                .Where(d => d.Durum == "Gelinmedi")
                .Include(d => d.Randevu)
                .ThenInclude(r => r.Uzman)
                .Include(d => d.Randevu.Islem)
                .Select(d => d.Randevu) // Sadece Randevu nesnelerini seçiyoruz
                .ToList();

            return View(gelinmeyenRandevular);
        }

        [HttpGet]
        public IActionResult GelinenRandevular()
        {
            var gelinenRandevular = _db.RandevuDurumlari
                .Where(d => d.Durum == "Gelindi")
                .Include(d => d.Randevu)
                .ThenInclude(r => r.Uzman)
                .Include(d => d.Randevu.Islem)
                .Select(d => d.Randevu) // Sadece Randevu nesnelerini seçiyoruz
                .ToList();

            return View(gelinenRandevular);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RandevuSil(int id)
        {
            var randevu = await _db.Randevular.FindAsync(id);
            if (randevu == null)
            {
                TempData["ErrorMessage"] = "Randevu bulunamadı.";
                return RedirectToAction("Randevular");
            }

            try
            {
                _db.Randevular.Remove(randevu);
                await _db.SaveChangesAsync();
                TempData["SuccessMessage"] = "Randevu başarıyla silindi.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Randevu silinirken bir hata oluştu.");
                TempData["ErrorMessage"] = "Randevu silinirken bir hata oluştu. Lütfen tekrar deneyin.";
            }

            return RedirectToAction("Randevular");
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
