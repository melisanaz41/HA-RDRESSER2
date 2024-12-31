﻿// <auto-generated />
using System;
using HAIRDRESSER2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HAIRDRESSER2.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241230201453_Seedkas")]
    partial class Seedkas
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HAIRDRESSER2.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Soyad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("HAIRDRESSER2.Models.CalismaSaati", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<TimeSpan>("BaslangicSaati")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("BitisSaati")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.ToTable("CalismaSaatleri", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BaslangicSaati = new TimeSpan(0, 8, 0, 0, 0),
                            BitisSaati = new TimeSpan(0, 13, 0, 0, 0)
                        },
                        new
                        {
                            Id = 2,
                            BaslangicSaati = new TimeSpan(0, 13, 0, 0, 0),
                            BitisSaati = new TimeSpan(0, 20, 0, 0, 0)
                        },
                        new
                        {
                            Id = 3,
                            BaslangicSaati = new TimeSpan(0, 8, 0, 0, 0),
                            BitisSaati = new TimeSpan(0, 20, 0, 0, 0)
                        });
                });

            modelBuilder.Entity("HAIRDRESSER2.Models.Islem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Fiyat")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Sure")
                        .HasColumnType("int");

                    b.Property<int?>("UzmanId")
                        .HasColumnType("int");

                    b.Property<int>("UzmanlikAlaniId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UzmanId");

                    b.HasIndex("UzmanlikAlaniId");

                    b.ToTable("Islemler", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Ad = "Saç Kesimi",
                            Fiyat = 200m,
                            Sure = 60,
                            UzmanlikAlaniId = 1
                        },
                        new
                        {
                            Id = 2,
                            Ad = "Tüm Saç Boyama",
                            Fiyat = 800m,
                            Sure = 180,
                            UzmanlikAlaniId = 1
                        },
                        new
                        {
                            Id = 3,
                            Ad = "Ombre",
                            Fiyat = 1500m,
                            Sure = 240,
                            UzmanlikAlaniId = 1
                        },
                        new
                        {
                            Id = 4,
                            Ad = "Saç Düzleştirme",
                            Fiyat = 1000m,
                            Sure = 120,
                            UzmanlikAlaniId = 1
                        },
                        new
                        {
                            Id = 5,
                            Ad = "Saç Bakım Maskesi",
                            Fiyat = 300m,
                            Sure = 45,
                            UzmanlikAlaniId = 1
                        },
                        new
                        {
                            Id = 6,
                            Ad = "Keratin Bakımı",
                            Fiyat = 1200m,
                            Sure = 150,
                            UzmanlikAlaniId = 1
                        },
                        new
                        {
                            Id = 7,
                            Ad = "Fön",
                            Fiyat = 200m,
                            Sure = 30,
                            UzmanlikAlaniId = 1
                        },
                        new
                        {
                            Id = 8,
                            Ad = "Kahkül Kesimi",
                            Fiyat = 150m,
                            Sure = 30,
                            UzmanlikAlaniId = 1
                        },
                        new
                        {
                            Id = 9,
                            Ad = "Maşa Yapma",
                            Fiyat = 300m,
                            Sure = 45,
                            UzmanlikAlaniId = 1
                        },
                        new
                        {
                            Id = 10,
                            Ad = "Gelin Makyajı",
                            Fiyat = 4000m,
                            Sure = 120,
                            UzmanlikAlaniId = 2
                        },
                        new
                        {
                            Id = 11,
                            Ad = "Özel Gün Makyajı",
                            Fiyat = 2000m,
                            Sure = 120,
                            UzmanlikAlaniId = 2
                        },
                        new
                        {
                            Id = 12,
                            Ad = "Kalıcı Makyaj",
                            Fiyat = 1500m,
                            Sure = 120,
                            UzmanlikAlaniId = 2
                        },
                        new
                        {
                            Id = 13,
                            Ad = "Göz Makyajı",
                            Fiyat = 800m,
                            Sure = 60,
                            UzmanlikAlaniId = 2
                        },
                        new
                        {
                            Id = 14,
                            Ad = "Doğal Günlük Makyaj",
                            Fiyat = 1000m,
                            Sure = 90,
                            UzmanlikAlaniId = 2
                        },
                        new
                        {
                            Id = 15,
                            Ad = "Manikür",
                            Fiyat = 150m,
                            Sure = 45,
                            UzmanlikAlaniId = 3
                        },
                        new
                        {
                            Id = 16,
                            Ad = "Pedikür",
                            Fiyat = 200m,
                            Sure = 60,
                            UzmanlikAlaniId = 3
                        },
                        new
                        {
                            Id = 17,
                            Ad = "Jel Tırnak",
                            Fiyat = 400m,
                            Sure = 90,
                            UzmanlikAlaniId = 3
                        },
                        new
                        {
                            Id = 18,
                            Ad = "Tırnak Dekorasyonu",
                            Fiyat = 500m,
                            Sure = 90,
                            UzmanlikAlaniId = 3
                        },
                        new
                        {
                            Id = 19,
                            Ad = "Yüz Bakımı",
                            Fiyat = 500m,
                            Sure = 60,
                            UzmanlikAlaniId = 4
                        },
                        new
                        {
                            Id = 20,
                            Ad = "Anti-Aging Bakımı",
                            Fiyat = 800m,
                            Sure = 90,
                            UzmanlikAlaniId = 4
                        },
                        new
                        {
                            Id = 21,
                            Ad = "Akne Tedavisi",
                            Fiyat = 700m,
                            Sure = 75,
                            UzmanlikAlaniId = 4
                        },
                        new
                        {
                            Id = 22,
                            Ad = "Cilt Leke Tedavisi",
                            Fiyat = 1200m,
                            Sure = 120,
                            UzmanlikAlaniId = 4
                        },
                        new
                        {
                            Id = 23,
                            Ad = "Göz Çevresi Bakımı",
                            Fiyat = 900m,
                            Sure = 90,
                            UzmanlikAlaniId = 4
                        },
                        new
                        {
                            Id = 24,
                            Ad = "Lazer Epilasyon - Küçük Bölge",
                            Fiyat = 300m,
                            Sure = 30,
                            UzmanlikAlaniId = 5
                        },
                        new
                        {
                            Id = 25,
                            Ad = "Lazer Epilasyon - Büyük Bölge",
                            Fiyat = 600m,
                            Sure = 60,
                            UzmanlikAlaniId = 5
                        },
                        new
                        {
                            Id = 26,
                            Ad = "Lazer Epilasyon - Tüm Vücut",
                            Fiyat = 2000m,
                            Sure = 120,
                            UzmanlikAlaniId = 5
                        },
                        new
                        {
                            Id = 27,
                            Ad = "Lazer Cilt Yenileme",
                            Fiyat = 1500m,
                            Sure = 90,
                            UzmanlikAlaniId = 5
                        });
                });

            modelBuilder.Entity("HAIRDRESSER2.Models.Randevu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("IslemId")
                        .HasColumnType("int");

                    b.Property<string>("KullaniciId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<TimeSpan>("Saat")
                        .HasColumnType("time");

                    b.Property<int>("Sure")
                        .HasColumnType("int");

                    b.Property<DateTime>("Tarih")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Ucret")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UzmanId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IslemId");

                    b.HasIndex("KullaniciId");

                    b.HasIndex("UzmanId");

                    b.ToTable("Randevular", (string)null);
                });

            modelBuilder.Entity("HAIRDRESSER2.Models.RandevuDurumu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Durum")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("IslemTarihi")
                        .HasColumnType("datetime2");

                    b.Property<int>("RandevuId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RandevuId");

                    b.ToTable("RandevuDurumlari", (string)null);
                });

            modelBuilder.Entity("HAIRDRESSER2.Models.Uzman", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CalismaSaatiId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EklenmeTarihi")
                        .HasColumnType("datetime2");

                    b.Property<string>("Soyad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UzmanlikAlaniId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CalismaSaatiId");

                    b.HasIndex("UzmanlikAlaniId");

                    b.ToTable("Uzmanlar", (string)null);
                });

            modelBuilder.Entity("HAIRDRESSER2.Models.UzmanlikAlani", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UzmanlikAlanlari");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Ad = "Saç"
                        },
                        new
                        {
                            Id = 2,
                            Ad = "Makyaj"
                        },
                        new
                        {
                            Id = 3,
                            Ad = "Tırnak"
                        },
                        new
                        {
                            Id = 4,
                            Ad = "Cilt Bakımı"
                        },
                        new
                        {
                            Id = 5,
                            Ad = "Lazer"
                        },
                        new
                        {
                            Id = 6,
                            Ad = "Kaş"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("HAIRDRESSER2.Models.Islem", b =>
                {
                    b.HasOne("HAIRDRESSER2.Models.Uzman", null)
                        .WithMany("Islemler")
                        .HasForeignKey("UzmanId");

                    b.HasOne("HAIRDRESSER2.Models.UzmanlikAlani", "UzmanlikAlani")
                        .WithMany("Islemler")
                        .HasForeignKey("UzmanlikAlaniId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UzmanlikAlani");
                });

            modelBuilder.Entity("HAIRDRESSER2.Models.Randevu", b =>
                {
                    b.HasOne("HAIRDRESSER2.Models.Islem", "Islem")
                        .WithMany()
                        .HasForeignKey("IslemId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("HAIRDRESSER2.Models.ApplicationUser", "Kullanici")
                        .WithMany("Randevular")
                        .HasForeignKey("KullaniciId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HAIRDRESSER2.Models.Uzman", "Uzman")
                        .WithMany()
                        .HasForeignKey("UzmanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Islem");

                    b.Navigation("Kullanici");

                    b.Navigation("Uzman");
                });

            modelBuilder.Entity("HAIRDRESSER2.Models.RandevuDurumu", b =>
                {
                    b.HasOne("HAIRDRESSER2.Models.Randevu", "Randevu")
                        .WithMany()
                        .HasForeignKey("RandevuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Randevu");
                });

            modelBuilder.Entity("HAIRDRESSER2.Models.Uzman", b =>
                {
                    b.HasOne("HAIRDRESSER2.Models.CalismaSaati", "CalismaSaati")
                        .WithMany("Uzmanlar")
                        .HasForeignKey("CalismaSaatiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HAIRDRESSER2.Models.UzmanlikAlani", "UzmanlikAlani")
                        .WithMany("Uzmanlar")
                        .HasForeignKey("UzmanlikAlaniId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("CalismaSaati");

                    b.Navigation("UzmanlikAlani");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("HAIRDRESSER2.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("HAIRDRESSER2.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HAIRDRESSER2.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("HAIRDRESSER2.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HAIRDRESSER2.Models.ApplicationUser", b =>
                {
                    b.Navigation("Randevular");
                });

            modelBuilder.Entity("HAIRDRESSER2.Models.CalismaSaati", b =>
                {
                    b.Navigation("Uzmanlar");
                });

            modelBuilder.Entity("HAIRDRESSER2.Models.Uzman", b =>
                {
                    b.Navigation("Islemler");
                });

            modelBuilder.Entity("HAIRDRESSER2.Models.UzmanlikAlani", b =>
                {
                    b.Navigation("Islemler");

                    b.Navigation("Uzmanlar");
                });
#pragma warning restore 612, 618
        }
    }
}
