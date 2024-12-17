using HAIRDRESSER2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Include için gerekli
using System.Linq;

namespace HAIRDRESSER2.Controllers
{
    public class UzmanController : Controller
    {
        private readonly ApplicationDbContext _db;

        // Constructor Injection kullanarak ApplicationDbContext'i alıyoruz
        public UzmanController(ApplicationDbContext db)
        {
            _db = db;
        }

        // Uzman Profili ve Çalışma Saatleri Getirme
        public IActionResult Profil(int id)
        {
            // Uzman ve ilişkili çalışma saatlerini ve uzmanlık alanını çekiyoruz
            var uzman = _db.Uzmanlar
                .Include(u => u.UzmanlikAlani)  // Uzmanlık alanını dahil et
                .Include(u => u.CalismaSaati)   // Çalışma saatlerini dahil et
                .FirstOrDefault(u => u.Id == id);

            if (uzman == null)
            {
                return NotFound(); // Uzman bulunamazsa hata döndür
            }

            return View(uzman);
        }

        // Controller Dispose
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
