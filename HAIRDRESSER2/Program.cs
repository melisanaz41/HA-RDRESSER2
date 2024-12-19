using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HAIRDRESSER2.Models;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Veritabaný yapýlandýrmasý
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Identity yapýlandýrmasý
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            // Þifre politikalarýný burada yapýlandýrabilirsiniz
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        // Denetleyiciler ve görünümler
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // Middleware yapýlandýrmasý
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication(); // Kullanýcý kimlik doðrulamasý
        app.UseAuthorization();  // Kullanýcý yetkilendirmesi

        // Tek bir default route
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        // Rolleri ve Admin kullanýcýyý seed etme iþlemi
        await SeedDatabaseAsync(app);

        app.Run();
    }

    // Veritabaný rolleri ve admin kullanýcýyý seed etme
    private static async Task SeedDatabaseAsync(WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            try
            {
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

                // Rolleri seed et
                await SeedRolesAsync(roleManager);

                // Admin kullanýcýyý seed et
                await SeedAdminUserAsync(userManager);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error seeding data: {ex.Message}");
            }
        }
    }

    // Rolleri ekleyen metod
    private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        string[] roleNames = { "Admin", "User" };

        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                var result = await roleManager.CreateAsync(new IdentityRole(roleName));
                if (!result.Succeeded)
                {
                    throw new Exception($"Rol oluþturulurken hata oluþtu: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }
    }

    // Admin kullanýcýyý ekleyen metod
    private static async Task SeedAdminUserAsync(UserManager<ApplicationUser> userManager)
    {
        string adminEmail = "admin@example.com";
        string adminPassword = "Admin123!"; // Þifre

        // Admin kullanýcýsýný kontrol et
        var existingAdmin = await userManager.FindByEmailAsync(adminEmail);
        if (existingAdmin == null)
        {
            // Yeni admin kullanýcý oluþtur
            var adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                Ad = "Admin", // ApplicationUser modelinizde varsa
                Soyad = "User", // ApplicationUser modelinizde varsa
                PhoneNumber = "1234567890"
            };

            var result = await userManager.CreateAsync(adminUser, adminPassword);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
                Console.WriteLine("Admin kullanýcý baþarýyla oluþturuldu.");
            }
            else
            {
                throw new Exception($"Admin kullanýcý oluþturulamadý: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }
        else
        {
            Console.WriteLine("Admin kullanýcý zaten mevcut.");
        }
    }
}
