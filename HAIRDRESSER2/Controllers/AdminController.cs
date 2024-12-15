using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using HAIRDRESSER2.Models;
using Microsoft.AspNetCore.Authorization;  // ApplicationUser'ı doğru şekilde dahil ettiğinizden emin olun

namespace HAIRDRESSER2.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager; // Giriş işlemi için SignInManager ekleniyor

       
        
            // Admin login sayfası
            public IActionResult Login()
            {
                return View();
            }

            // Admin giriş işlemi
            [HttpPost]
            public async Task<IActionResult> Login(LoginViewModel model)
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                    {
                        // Admin kontrolü yapılabilir
                        if (await _userManager.IsInRoleAsync(user, "Admin"))
                        {
                            // Admin girişini başarılı yap
                            await _signInManager.SignInAsync(user, isPersistent: false);
                            return RedirectToAction("AdminDashboard", "Admin"); // Admin paneline yönlendir
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
