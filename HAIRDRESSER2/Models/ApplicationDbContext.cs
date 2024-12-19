using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace HAIRDRESSER2.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // DbSet tanımlamaları
        public DbSet<Uzman> Uzmanlar { get; set; }
        public DbSet<Randevu> Randevular { get; set; }
        public DbSet<CalismaSaati> CalismaSaatleri { get; set; }
        public DbSet<UzmanlikAlani> UzmanlikAlanlari { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Identity yapılandırmalarını korur

            // Tablo isimlerini özelleştirme
            modelBuilder.Entity<Uzman>().ToTable("Uzmanlar");
            modelBuilder.Entity<Randevu>().ToTable("Randevular");
            modelBuilder.Entity<CalismaSaati>().ToTable("CalismaSaatleri");

            // Uzman ile UzmanlikAlani arasındaki ilişki
            modelBuilder.Entity<Uzman>()
                .HasOne(u => u.UzmanlikAlani)
                .WithMany(ua => ua.Uzmanlar)
                .HasForeignKey(u => u.UzmanlikAlaniId)
                .OnDelete(DeleteBehavior.Restrict); // İlişki silinmesi için davranışı belirt

            // Uzman ile CalismaSaati arasındaki ilişki
 // Uzman silinirse çalışma saatleri de silinsin

            // UzmanlikAlanlari ön tanımlı veriler
            modelBuilder.Entity<UzmanlikAlani>().HasData(
                new UzmanlikAlani { Id = 1, Ad = "Saç" },
                new UzmanlikAlani { Id = 2, Ad = "Makyaj" },
                new UzmanlikAlani { Id = 3, Ad = "Tırnak" },
                new UzmanlikAlani { Id = 4, Ad = "Cilt Bakımı" },
                new UzmanlikAlani { Id = 5, Ad = "Lazer" }
            );

            // Çalışma saatleri ön tanımlı veriler
            modelBuilder.Entity<CalismaSaati>().HasData(
                new CalismaSaati
                {
                    Id = 1,
                    BaslangicSaati = new TimeSpan(8, 0, 0), // Sabah 8
                    BitisSaati = new TimeSpan(13, 0, 0)    // Öğlen 1
                },
                new CalismaSaati
                {
                    Id = 2,
                    BaslangicSaati = new TimeSpan(13, 0, 0), // Öğlen 1
                    BitisSaati = new TimeSpan(20, 0, 0)    // Akşam 8
                },
                new CalismaSaati
                {
                    Id = 3,
                    BaslangicSaati = new TimeSpan(8, 0, 0), // Sabah 8
                    BitisSaati = new TimeSpan(20, 0, 0)    // Akşam 8
                }
            );
        }
    }
}
