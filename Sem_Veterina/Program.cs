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

// P�id�n� connection stringu
string connectionString = builder.Configuration.GetConnectionString("OracleDbContext");
Console.WriteLine(connectionString);

// Konfigurace DbContext pro Oracle s pou�it�m connection stringu
builder.Services.AddDbContext<OracleDbContext>(options =>
    options.UseOracle(connectionString));
builder.Services.AddTransient<DbTestService>();
builder.Services.AddTransient<KlinikaService>();
builder.Services.AddTransient<ZvireService>();
builder.Services.AddTransient<MajitelService>();
builder.Services.AddTransient<ZdravotniAkceService>(); // P�idejte tuto ��dku pro ZdravotniAkceService
builder.Services.AddTransient<PristrojeService>();
builder.Services.AddTransient<LecbaService>();
builder.Services.AddTransient<LekyService>();
builder.Services.AddTransient<PersonalService>();
builder.Services.AddTransient<UzivatelService>();

// P�id�n� MVC a dal��ch slu�eb
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Testov�n� p�ipojen� k datab�zi
using (var connection = new OracleConnection(connectionString))
{
    try
    {
        connection.Open();
        Console.WriteLine("P�ipojen� k datab�zi bylo �sp�n�!");
    }
    catch (Exception ex)
    {
        Console.WriteLine("P�ipojen� k datab�zi se nezda�ilo.");
        Console.WriteLine($"Chyba: {ex.Message}");
    }
}

// Mo�nost inicializace DbContextu p�i spu�t�n� aplikace
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<OracleDbContext>();
        // P��padn� inicializace datab�ze nebo migrace
        // context.Database.Migrate();

        // Z�sk�n� instance KlinikaService ZV��ATA DI kontejneru
        var klinikaService = services.GetService<KlinikaService>();
        if (klinikaService != null)
        {
            // Vol�n� metody GetAllKlinikyAsync a v�pis do konzole
            await DisplayAllKliniky(klinikaService);
        }
        else
        {
            Console.WriteLine("KlinikaService nelze na��st z DI kontejneru.");
        }
        List<ZVIRATA> ZvireList = await services.GetService<ZvireService>().GetAllZvirataAsync();
        foreach (var zvirata in ZvireList) { Console.WriteLine(zvirata.DRUH); }

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
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();

// Asynchronn� metoda pro v�pis v�ech klinik
static async Task DisplayAllKliniky(KlinikaService klinikaService)
{
    try
    {
        List<KLINIKY> kliniky = await klinikaService.GetAllKlinikyAsync();

        Console.WriteLine("Seznam klinik:");
        foreach (var klinika in kliniky)
        {
            Console.WriteLine($"ID: {klinika.ID_KLINIKA}, N�zev: {klinika.NÁZEV}, Adresa: {klinika.ADRESA}, Telefon: {klinika.TELEFONNÍ_ČÍSLO}, Email: {klinika.EMAIL}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Chyba p�i na��t�n� klinik: {ex.Message}");
    }

}