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
        public DbSet<Islem> Islemler { get; set; }
        public DbSet<Randevu> Randevular { get; set; }
        public DbSet<CalismaSaati> CalismaSaatleri { get; set; }
        public DbSet<UzmanlikAlani> UzmanlikAlanlari { get; set; }
        public DbSet<RandevuDurumu> RandevuDurumlari { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Identity yapılandırmalarını korur

            // Tablo isimlerini özelleştirme
            modelBuilder.Entity<Uzman>().ToTable("Uzmanlar");
            modelBuilder.Entity<Randevu>().ToTable("Randevular");
            modelBuilder.Entity<CalismaSaati>().ToTable("CalismaSaatleri");
            modelBuilder.Entity<Islem>().ToTable("Islemler");
            modelBuilder.Entity<RandevuDurumu>().ToTable("RandevuDurumlari");




            modelBuilder.Entity<Randevu>()
        .HasOne(r => r.Islem)
        .WithMany()
        .HasForeignKey(r => r.IslemId)
        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Randevu>()
                .Property(r => r.Ucret)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Randevu>()
                .Property(r => r.Sure)
                .IsRequired();



            modelBuilder.Entity<Islem>()
    .Property(i => i.Fiyat)
    .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Islem>()
        .HasOne(i => i.Uzman)
        .WithMany(u => u.Islemler)
        .HasForeignKey(i => i.UzmanId)
        .OnDelete(DeleteBehavior.SetNull); // Uzman silinince UzmanId null yapılır
            // Uzman ile UzmanlikAlani arasındaki ilişki
            modelBuilder.Entity<Uzman>()
                .HasOne(u => u.UzmanlikAlani)
                .WithMany(ua => ua.Uzmanlar)
                .HasForeignKey(u => u.UzmanlikAlaniId)
                .OnDelete(DeleteBehavior.Restrict); // İlişki silinmesi için davranışı belirt

            // Uzman ile CalismaSaati arasındaki ilişki
            // Uzman silinirse çalışma saatleri de silinsin

            modelBuilder.Entity<Islem>().HasData(

            new Islem { Id = 1, Ad = "Saç Kesimi", Fiyat = 200, UzmanlikAlaniId = 1, Sure = 60 },
            new Islem { Id = 2, Ad = "Tüm Saç Boyama", Fiyat = 800, UzmanlikAlaniId = 1, Sure = 180 },
            new Islem { Id = 3, Ad = "Ombre", Fiyat = 1500, UzmanlikAlaniId = 1, Sure = 240 },
            new Islem { Id = 4, Ad = "Saç Düzleştirme", Fiyat = 1000, UzmanlikAlaniId = 1, Sure = 120 },
            new Islem { Id = 5, Ad = "Saç Bakım Maskesi", Fiyat = 300, UzmanlikAlaniId = 1, Sure = 45 },
            new Islem { Id = 6, Ad = "Keratin Bakımı", Fiyat = 1200, UzmanlikAlaniId = 1, Sure = 150 },
            new Islem { Id = 7, Ad = "Fön", Fiyat = 200, UzmanlikAlaniId = 1, Sure = 30 },
            new Islem { Id = 8, Ad = "Kahkül Kesimi", Fiyat = 150, UzmanlikAlaniId = 1, Sure = 30 },
            new Islem { Id = 9, Ad = "Maşa Yapma", Fiyat = 300, UzmanlikAlaniId = 1, Sure = 45 },
            new Islem { Id = 10, Ad = "Gelin Makyajı", Fiyat = 4000, UzmanlikAlaniId = 2, Sure = 120 },
            new Islem { Id = 11, Ad = "Özel Gün Makyajı", Fiyat = 2000, UzmanlikAlaniId = 2, Sure = 120 },
            new Islem { Id = 12, Ad = "Kalıcı Makyaj", Fiyat = 1500, UzmanlikAlaniId = 2, Sure = 120 },
            new Islem { Id = 13, Ad = "Göz Makyajı", Fiyat = 800, UzmanlikAlaniId = 2, Sure = 60 },
            new Islem { Id = 14, Ad = "Doğal Günlük Makyaj", Fiyat = 1000, UzmanlikAlaniId = 2, Sure = 90 },
            new Islem { Id = 15, Ad = "Manikür", Fiyat = 150, UzmanlikAlaniId = 3, Sure = 45 },
            new Islem { Id = 16, Ad = "Pedikür", Fiyat = 200, UzmanlikAlaniId = 3, Sure = 60 },
            new Islem { Id = 17, Ad = "Jel Tırnak", Fiyat = 400, UzmanlikAlaniId = 3, Sure = 90 },
            new Islem { Id = 18, Ad = "Tırnak Dekorasyonu", Fiyat = 500, UzmanlikAlaniId = 3, Sure = 90 },
            new Islem { Id = 19, Ad = "Yüz Bakımı", Fiyat = 500, UzmanlikAlaniId = 4, Sure = 60 },
            new Islem { Id = 20, Ad = "Anti-Aging Bakımı", Fiyat = 800, UzmanlikAlaniId = 4, Sure = 90 },
            new Islem { Id = 21, Ad = "Akne Tedavisi", Fiyat = 700, UzmanlikAlaniId = 4, Sure = 75 },
            new Islem { Id = 22, Ad = "Cilt Leke Tedavisi", Fiyat = 1200, UzmanlikAlaniId = 4, Sure = 120 },
            new Islem { Id = 23, Ad = "Göz Çevresi Bakımı", Fiyat = 900, UzmanlikAlaniId = 4, Sure = 90 },
            new Islem { Id = 24, Ad = "Lazer Epilasyon - Küçük Bölge", Fiyat = 300, UzmanlikAlaniId = 5, Sure = 30 },
            new Islem { Id = 25, Ad = "Lazer Epilasyon - Büyük Bölge", Fiyat = 600, UzmanlikAlaniId = 5, Sure = 60 },
            new Islem { Id = 26, Ad = "Lazer Epilasyon - Tüm Vücut", Fiyat = 2000, UzmanlikAlaniId = 5, Sure = 120 },
            new Islem { Id = 27, Ad = "Lazer Cilt Yenileme", Fiyat = 1500, UzmanlikAlaniId = 5, Sure = 90 }




);
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