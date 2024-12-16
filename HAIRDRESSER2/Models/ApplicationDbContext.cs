using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace HAIRDRESSER2.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Tabloları temsil eden DbSet özellikleri
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Uzman> Uzmanlar { get; set; }
        public DbSet<Randevu> Randevular { get; set; }
        public DbSet<CalismaSaati> CalismaSaatleri { get; set; }

        // Fluent API ile yapılandırma
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Identity yapılandırmalarını korur

            // Tabloların isimlerini özelleştirme (isteğe bağlı)
            modelBuilder.Entity<Kullanici>().ToTable("Kullanicilar");
            modelBuilder.Entity<Uzman>().ToTable("Uzmanlar");
            modelBuilder.Entity<Randevu>().ToTable("Randevular");
            modelBuilder.Entity<CalismaSaati>().ToTable("CalismaSaatleri");

            // Randevu ile Uzman arasındaki ilişki örneği
            modelBuilder.Entity<Randevu>()
                .HasOne(r => r.Uzman)
                .WithMany(u => u.Randevular)
                .HasForeignKey(r => r.UzmanId);

            // İsteğe bağlı: Kullanıcı ve diğer varlıklar arasındaki ilişkiler
            // modelBuilder.Entity<Kullanici>()
            //     .HasMany(k => k.Randevular)
            //     .WithOne(r => r.Kullanici)
            //     .HasForeignKey(r => r.KullaniciId);
        }
    }
}
