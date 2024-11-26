using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace HAİRDRESSER2.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Uzman> Uzmanlar { get; set; }
        public DbSet<Randevu> Randevular { get; set; }
        public DbSet<CalismaSaati> CalismaSaatleri { get; set; }
    }
}
