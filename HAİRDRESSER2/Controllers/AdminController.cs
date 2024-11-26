using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HAİRDRESSER2.Controllers
{
    [Authorize(Roles = "Admin")] // Bu controller'a yalnızca "Admin" rolündeki kullanıcılar erişebilir
    public class AdminController : Controller
    {
        public IActionResult Panel()
        {
            return View();
        }

        public IActionResult KullaniciYonetimi()
        {
            // Kullanıcı yönetimi işlemleri burada yapılır
            return View();
        }

        public IActionResult RandevuYonetimi()
        {
            // Randevu yönetimi işlemleri burada yapılır
            return View();
        }
    }
}
