using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace HAIRDRESSER2.Models
{
    public class Data
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>(); // RoleManager
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>(); // UserManager

            // Rolleri oluştur (Admin ve User)
            string[] roles = { "Admin", "User" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var roleResult = await roleManager.CreateAsync(new IdentityRole(role));
                    if (!roleResult.Succeeded)
                    {
                        Console.WriteLine($"Rol oluşturulamadı: {role}");
                    }
                }
            }

            // Admin kullanıcıyı oluştur
            var adminEmail = "admin@example.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = "admin",
                    Email = adminEmail,
                    Ad = "Admin",
                    Soyad = "User"
                };

                // Kullanıcıyı oluştur ve Admin rolüne ata
                var result = await userManager.CreateAsync(adminUser, "Admin123!");
                if (result.Succeeded)
                {
                    var addToRoleResult = await userManager.AddToRoleAsync(adminUser, "Admin");
                    if (!addToRoleResult.Succeeded)
                    {
                        Console.WriteLine("Admin rolüne kullanıcı eklenemedi.");
                    }
                }
                else
                {
                    // Kullanıcı oluşturulamadıysa hata mesajlarını yazdır
                    Console.WriteLine("Admin kullanıcısı oluşturulamadı:");
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"Hata: {error.Description}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Admin kullanıcısı zaten mevcut.");
            }
        }
    }
}
