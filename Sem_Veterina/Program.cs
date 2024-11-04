using Microsoft.AspNetCore.Hosting;
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

var builder = WebApplication.CreateBuilder(args);

// Pøidání connection stringu
string connectionString = builder.Configuration.GetConnectionString("OracleDbContext");
Console.WriteLine(connectionString);

// Konfigurace DbContext pro Oracle s použitím connection stringu
builder.Services.AddDbContext<OracleDbContext>(options =>
    options.UseOracle(connectionString));
builder.Services.AddTransient<DbTestService>();
builder.Services.AddTransient<KlinikaService>();
builder.Services.AddTransient<ZvireService>();
builder.Services.AddTransient<MajitelService>();
builder.Services.AddTransient<ZdravotniAkceService>(); // Pøidejte tuto øádku pro ZdravotniAkceService
builder.Services.AddTransient<PristrojeService>();
builder.Services.AddTransient<LecbaService>();
builder.Services.AddTransient<LekyService>();
builder.Services.AddTransient<PersonalService>();

// Pøidání MVC a dalších služeb
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Testování pøipojení k databázi
using (var connection = new OracleConnection(connectionString))
{
    try
    {
        connection.Open();
        Console.WriteLine("Pøipojení k databázi bylo úspìšné!");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Pøipojení k databázi se nezdaøilo.");
        Console.WriteLine($"Chyba: {ex.Message}");
    }
}

// Možnost inicializace DbContextu pøi spuštìní aplikace
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<OracleDbContext>();
        // Pøípadná inicializace databáze nebo migrace
        // context.Database.Migrate();

        // Získání instance KlinikaService ZVÍØATA DI kontejneru
        var klinikaService = services.GetService<KlinikaService>();
        if (klinikaService != null)
        {
            // Volání metody GetAllKlinikyAsync a výpis do konzole
            await DisplayAllKliniky(klinikaService);
        }
        else
        {
            Console.WriteLine("KlinikaService nelze naèíst z DI kontejneru.");
        }
        List<ZVIRATA> ZvireList = await services.GetService<ZvireService>().GetAllZvirataAsync();
        foreach(var zvirata in ZvireList) { Console.WriteLine(zvirata.DRUH); }
        
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error during database initialization: {ex.Message}");
    }
}

// Konfigurace HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

// Asynchronní metoda pro výpis všech klinik
static async Task DisplayAllKliniky(KlinikaService klinikaService)
{
    try
    {
        List<KLINIKY> kliniky = await klinikaService.GetAllKlinikyAsync();

        Console.WriteLine("Seznam klinik:");
        foreach (var klinika in kliniky)
        {
            Console.WriteLine($"ID: {klinika.ID_KLINIKA}, Název: {klinika.NÁZEV}, Adresa: {klinika.ADRESA}, Telefon: {klinika.TELEFONNÍ_ÈÍSLO}, Email: {klinika.EMAIL}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Chyba pøi naèítání klinik: {ex.Message}");
    }
   
}