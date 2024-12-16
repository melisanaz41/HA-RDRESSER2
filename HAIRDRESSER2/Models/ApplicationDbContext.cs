﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HAIRDRESSER2.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Diğer tablolarınızı eklemek için DbSet özellikleri
        public DbSet<Uzman> Uzmanlar { get; set; }
        public DbSet<Randevu> Randevular { get; set; }
        public DbSet<CalismaSaati> CalismaSaatleri { get; set; }
        public DbSet<UzmanlikAlani> UzmanlikAlanlari { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Identity yapılandırmalarını korur

            // Diğer tabloların isimlerini özelleştirme (isteğe bağlı)
            modelBuilder.Entity<Uzman>().ToTable("Uzmanlar");
            modelBuilder.Entity<Randevu>().ToTable("Randevular");
            modelBuilder.Entity<CalismaSaati>().ToTable("CalismaSaatleri");
            // UzmanlikAlani ile Uzman arasında ilişki tanımlama
            modelBuilder.Entity<Uzman>()
                .HasOne(u => u.UzmanlikAlani)
                .WithMany(ua => ua.Uzmanlar)
                .HasForeignKey(u => u.UzmanlikAlaniId);

            // Uzmanlık alanlarını ön tanımlı olarak eklemek
            modelBuilder.Entity<UzmanlikAlani>().HasData(
                new UzmanlikAlani { Id = 1, Ad = "Saç" },
                new UzmanlikAlani { Id = 2, Ad = "Makyaj" },
                new UzmanlikAlani { Id = 3, Ad = "Tırnak" },
                new UzmanlikAlani { Id = 4, Ad = "Cilt Bakımı" });

        }
    }
}