using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HAIRDRESSER2.Models;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Veritaban� yap�land�rmas�
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Identity yap�land�rmas�
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            // �ifre politikalar�n� burada yap�land�rabilirsiniz
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        // Denetleyiciler ve g�r�n�mler
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // Middleware yap�land�rmas�
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication(); // Kullan�c� kimlik do�rulamas�
        app.UseAuthorization();  // Kullan�c� yetkilendirmesi

        // Tek bir default route
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        // Rolleri ve Admin kullan�c�y� seed etme i�lemi
        await SeedDatabaseAsync(app);

        app.Run();
    }

    // Veritaban� rolleri ve admin kullan�c�y� seed etme
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

                // Admin kullan�c�y� seed et
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
                    throw new Exception($"Rol olu�turulurken hata olu�tu: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }
    }

    // Admin kullan�c�y� ekleyen metod
    private static async Task SeedAdminUserAsync(UserManager<ApplicationUser> userManager)
    {
        string adminEmail = "admin@example.com";
        string adminPassword = "Admin123!"; // �ifre

        // Admin kullan�c�s�n� kontrol et
        var existingAdmin = await userManager.FindByEmailAsync(adminEmail);
        if (existingAdmin == null)
        {
            // Yeni admin kullan�c� olu�tur
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
                Console.WriteLine("Admin kullan�c� ba�ar�yla olu�turuldu.");
            }
            else
            {
                throw new Exception($"Admin kullan�c� olu�turulamad�: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }
        else
        {
            Console.WriteLine("Admin kullan�c� zaten mevcut.");
        }
    }
}
