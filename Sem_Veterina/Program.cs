

// Add services to the container.

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Sem_Veterina;
using System;
using Sem_Veterina.Controllers;
using Oracle.ManagedDataAccess.Client;

var builder = WebApplication.CreateBuilder(args);

// Pøidání connection stringu
string connectionString = builder.Configuration.GetConnectionString("OracleDbContext");
Console.WriteLine(connectionString);

// Konfigurace DbContext pro Oracle s použitím connection stringu
builder.Services.AddDbContext<OracleDbContext>(options =>
    options.UseOracle(connectionString));
builder.Services.AddTransient<DbTestService>();

// Pøidání MVC a dalších služeb
builder.Services.AddControllersWithViews();

var app = builder.Build();
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