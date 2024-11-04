using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Sem_Veterina;
using Sem_Veterina.Controllers;
using Oracle.ManagedDataAccess.Client;
using Sem_Veterina.CRUD;
using Sem_Veterina.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // Přidání connection stringu
        string connectionString = Configuration.GetConnectionString("OracleDbContext");
        Console.WriteLine(connectionString);

        // Konfigurace DbContext pro Oracle
        services.AddDbContext<OracleDbContext>(options =>
            options.UseOracle(connectionString));

        // Registrace služeb do DI kontejneru
        services.AddTransient<DbTestService>();
        services.AddTransient<KlinikaService>();
        services.AddTransient<ZvireService>();
        services.AddTransient<MajitelService>();
        services.AddTransient<ZdravotniAkceService>();
        services.AddTransient<PristrojeService>();
        services.AddTransient<LecbaService>();
        services.AddTransient<LekyService>();
        services.AddTransient<PersonalService>();

        // Přidání MVC a dalších služeb
        services.AddControllersWithViews();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Testování připojení k databázi
        TestDatabaseConnection(Configuration.GetConnectionString("OracleDbContext")).Wait();

        // Inicializace služeb a databáze
        InitializeDatabase(app).Wait();

        if (!env.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }

    private static async Task TestDatabaseConnection(string connectionString)
    {
        using var connection = new OracleConnection(connectionString);
        try
        {
            connection.Open();
            Console.WriteLine("Připojení k databázi bylo úspěšné!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Připojení k databázi se nezdařilo.");
            Console.WriteLine($"Chyba: {ex.Message}");
        }
    }

    private static async Task InitializeDatabase(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<OracleDbContext>();
            // Možné spuštění migrací
            // context.Database.Migrate();

            // Získání instance KlinikaService
            var klinikaService = services.GetService<KlinikaService>();
            if (klinikaService != null)
            {
                await DisplayAllKliniky(klinikaService);
            }
            else
            {
                Console.WriteLine("KlinikaService nelze načíst z DI kontejneru.");
            }

            var zvireService = services.GetService<ZvireService>();
            if (zvireService != null)
            {
                List<ZVIRATA> ZvireList = await zvireService.GetAllZvirataAsync();
                foreach (var zvirata in ZvireList)
                {
                    Console.WriteLine(zvirata.DRUH);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during database initialization: {ex.Message}");
        }
    }

    private static async Task DisplayAllKliniky(KlinikaService klinikaService)
    {
        try
        {
            List<KLINIKY> kliniky = await klinikaService.GetAllKlinikyAsync();

            Console.WriteLine("Seznam klinik:");
            foreach (var klinika in kliniky)
            {
                Console.WriteLine($"ID: {klinika.ID_KLINIKA}, Název: {klinika.NÁZEV}, Adresa: {klinika.ADRESA}, Telefon: {klinika.TELEFONNÍ_ČÍSLO}, Email: {klinika.EMAIL}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Chyba při načítání klinik: {ex.Message}");
        }
    }
}
