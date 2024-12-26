using HAIRDRESSER2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Veritabanı yapılandırması
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Identity yapılandırması
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            // Şifre politikalarını yapılandırma
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

        // Middleware yapılandırması
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }
        app.UseStaticFiles(); // Statik dosyaları yükleme (wwwroot)
        app.UseRouting();     // Rota yapılandırması
        app.UseAuthentication(); // Kimlik doğrulama
        app.UseAuthorization();  // Yetkilendirme

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        // Kök URL yönlendirmesi
        app.MapGet("/", context =>
        {
            context.Response.Redirect("/Home/Index", permanent: false);
            return Task.CompletedTask;
        });

        // Rolleri ve admin kullanıcıyı seed etme
        await SeedDatabaseAsync(app);
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.Run();
    }

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

                // Admin kullanıcıyı seed et
                await SeedAdminUserAsync(userManager);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error seeding data: {ex.Message}");
            }
        }
    }

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
                    throw new Exception($"Rol oluşturulurken hata oluştu: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }
    }

    private static async Task SeedAdminUserAsync(UserManager<ApplicationUser> userManager)
    {
        string adminEmail = "admin@example.com";
        string adminPassword = "Admin123!"; // Şifre

        var existingAdmin = await userManager.FindByEmailAsync(adminEmail);
        if (existingAdmin == null)
        {
            var adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                Ad = "Admin",
                Soyad = "User",
                PhoneNumber = "1234567890"
            };

            var result = await userManager.CreateAsync(adminUser, adminPassword);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
                Console.WriteLine("Admin kullanıcı başarıyla oluşturuldu.");
            }
            else
            {
                throw new Exception($"Admin kullanıcı oluşturulamadı: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }
        else
        {
            Console.WriteLine("Admin kullanıcı zaten mevcut.");
        }
    }
}
