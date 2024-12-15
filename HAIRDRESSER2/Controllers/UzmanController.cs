
using HAIRDRESSER2.Models;
using Microsoft.AspNetCore.Mvc;

namespace HAIRDRESSER2.Controllers
{
    public class UzmanController : Controller
    {
        private readonly ApplicationDbContext _db;

        // Constructor Injection kullanarak ApplicationDbContext'i alıyoruz
        public UzmanController(ApplicationDbContext db)
        {
            _db = db;
        } //eklendi

        public IActionResult Profil(int id)
        {
            var uzman = _db.Uzmanlar.Find(id);
            return View(uzman);
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
