using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

using Sem_Veterina.Models;
using Sem_Veterina.Entity;
using Sem_Veterina.CRUD;

namespace Sem_Veterina.Controllers
{
    public class LoginController : Controller
    {
        private readonly UzivatelService _uzivatelService;

        public LoginController(
            UzivatelService uzivatelService
        )
        {
            _uzivatelService = uzivatelService;
        }

        public IActionResult Index()
        {
            var viewModel = new LoginViewModel
            {
            };
            return View("Index", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(string Username, string Heslo)
        {
            // Validace vstupů
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Heslo))
            {
                ModelState.AddModelError("", "Uživatelské jméno a heslo jsou povinné.");
                return View("Index"); // Nebo vrátit modální chybu, pokud používáte AJAX
            }

            // Kontrola, zda uživatel již existuje
            var existujiciUzivatel = await _uzivatelService.GetUzivatelByUsernameAsync(Username);
            if (existujiciUzivatel != null)
            {
                ModelState.AddModelError("Username", "Uživatel s tímto jménem již existuje.");
                return View(); // Vrať se na stránku registrace s chybou
            }

            // Vytvoření nového uživatele
            var novyUzivatel = new UZIVATEL
            {
                USERNAME = Username,
                HESLO = BCrypt.Net.BCrypt.HashPassword(Heslo), // Použijte šifrování hesla
                ID_ROLE = 2
            };

            await _uzivatelService.AddUzivatelAsync(novyUzivatel);

            // Přesměrování na přihlašovací stránku
            TempData["SuccessMessage"] = "Účet byl úspěšně vytvořen. Nyní se můžete přihlásit.";
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Login(string Username, string Heslo)
        {
            // Validace vstupů
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Heslo))
            {
                ModelState.AddModelError("", "Uživatelské jméno a heslo jsou povinné.");
                return View("Index");
            }

            // Získání uživatele z databáze podle uživatelského jména
            var uzivatel = await _uzivatelService.GetUzivatelByUsernameAsync(Username);
            if (uzivatel == null)
            {
                ModelState.AddModelError("Username", "Tento uživatel neexistuje.");
                //return View();
                return View("Index");
            }

            // Porovnání zadaného hesla s hashovaným heslem z databáze
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(Heslo, uzivatel.HESLO);
            if (!isPasswordValid)
            {
                ModelState.AddModelError("Heslo", "Nesprávné heslo.");
                //return View();
                return View("Index");
            }

            // Přihlášení uživatele (uložení do session nebo cookies)
            HttpContext.Session.SetString("UserId", uzivatel.ID_UZIVATEL.ToString());
            HttpContext.Session.SetString("Role", uzivatel.ID_ROLE.ToString());

            //todo : smerovani podle role 
            // if uzivatel.ROLE = "Admin" tak presmerovani pro admina.....a tak dale

            // Přesměrování na domovskou stránku nebo dashboard
            return RedirectToAction("Index", "Home");
        }

    }
}