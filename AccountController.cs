using HAIRDRESSER2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HAIRDRESSER2.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        // Admin Login İşlemi
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Kullanıcıyı email ile bul
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Kullanıcı bulunamadı.");
                return View(model);
            }

            // Kullanıcı adı ve şifre doğrulama
            var result = await _signInManager.PasswordSignInAsync(user.Email, model.Password, isPersistent: false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Geçersiz giriş bilgileri.");
                return View(model);
            }

            // Kullanıcının rollerini kontrol et
            var roles = await _userManager.GetRolesAsync(user);
            if (!roles.Contains("Admin"))
            {
                ModelState.AddModelError(string.Empty, "Bu kullanıcı admin yetkisine sahip değil.");
                await _signInManager.SignOutAsync();
                return View(model);
            }

            // Giriş başarılı, Admin Kontrol Paneline yönlendir
            return RedirectToAction("AdminControl");
        }
    }
}