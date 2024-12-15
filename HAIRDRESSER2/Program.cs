using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
//using AspNetCore.Identity.Database;
using HAIRDRESSER2.Models;  // Bu sat�r eksik olmas�n

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddEndpointsApiExplorer();
        //builder.Services.AddSwaggerGen();
        // Veritaban� yap�land�rmas�
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddIdentityCore<RegisterViewModel>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddApiEndpoints();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddAuthorization();
        builder.Services.AddAuthentication().AddCookie(IdentityConstants.ApplicationScheme);
        // Identity yap�land�rmas�
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        // Denetleyiciler ve g�r�n�mler
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // Rolleri ve ba�lang�� verilerini ekle
        app.UseHttpsRedirection();

        if (app.Environment.IsDevelopment())
        {
            //app.UseS
        }

        // Middleware yap�land�rmas�
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }
        //deneme yap�l�yor
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();

        // Rolleri ekleyen metod
        async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = { "Admin", "User" };

            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                await Data.InitializeAsync(services);  // Veritaban� ve admin kullan�c�lar�n� olu�tur
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Veri olu�turulurken hata: {ex.Message}");
            }
        }
    }
}