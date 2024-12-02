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
            //return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.RegisterModal = true;
                return View("Index", model);
            }

            // Kontrola uživatele
            var existujiciUzivatel = await _uzivatelService.GetUzivatelByUsernameAsync(model.RegisterUsername);
            if (existujiciUzivatel != null)
            {
                ModelState.AddModelError("Username", "Uživatel s tímto jménem již existuje.");
                ViewBag.RegisterModal = true;
                return View("Index", model);
            }

            // Vytvoření uživatele
            var novyUzivatel = new UZIVATEL
            {
                USERNAME = model.RegisterUsername,
                HESLO = BCrypt.Net.BCrypt.HashPassword(model.RegisterPassword),
                ID_ROLE = 2
            };
            await _uzivatelService.AddUzivatelAsync(novyUzivatel);

            TempData["SuccessMessage"] = "Účet byl úspěšně vytvořen. Nyní se můžete přihlásit.";
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            // Validace vstupů
            if (!ModelState.IsValid)
            {
                ViewBag.LoginModal = true; // Otevření modalu při chybě
                return View("Index", model);
            }

            // Získání uživatele z databáze
            var uzivatel = await _uzivatelService.GetUzivatelByUsernameAsync(model.LoginUsername);
            if (uzivatel == null)
            {
                ModelState.AddModelError("Username", "Tento uživatel neexistuje.");
                ViewBag.LoginModal = true;
                return View("Index", model);
            }

            // Kontrola hesla
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(model.LoginPassword, uzivatel.HESLO);
            if (!isPasswordValid)
            {
                ModelState.AddModelError("Heslo", "Nesprávné heslo.");
                ViewBag.LoginModal = true;
                return View("Index", model);
            }

            // Přihlášení
            HttpContext.Session.SetString("UserId", uzivatel.ID_UZIVATEL.ToString());
            HttpContext.Session.SetString("Role", uzivatel.ID_ROLE.ToString());

            // Přesměrování podle role
            return uzivatel.ID_ROLE switch
            {
                1 => RedirectToAction("Admin", "Index"),
                _ => RedirectToAction("Home", "Index"),
            };
        }
    }
}

// [HttpPost]
// public async Task<IActionResult> Register(string Username, string Heslo)
// {
//     // Validace vstupů
//     if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Heslo))
//     {
//         ModelState.AddModelError("", "Uživatelské jméno a heslo jsou povinné.");
//         return View("Index"); // Nebo vrátit modální chybu, pokud používáte AJAX
//     }

//     // Kontrola, zda uživatel již existuje
//     var existujiciUzivatel = await _uzivatelService.GetUzivatelByUsernameAsync(Username);
//     if (existujiciUzivatel != null)
//     {
//         ModelState.AddModelError("Username", "Uživatel s tímto jménem již existuje.");
//         return View(); // Vrať se na stránku registrace s chybou
//     }

//     // Vytvoření nového uživatele
//     var novyUzivatel = new UZIVATEL
//     {
//         USERNAME = Username,
//         HESLO = BCrypt.Net.BCrypt.HashPassword(Heslo), // Použijte šifrování hesla
//         ID_ROLE = 2
//     };

//     await _uzivatelService.AddUzivatelAsync(novyUzivatel);

//     // Přesměrování na přihlašovací stránku
//     TempData["SuccessMessage"] = "Účet byl úspěšně vytvořen. Nyní se můžete přihlásit.";
//     return RedirectToAction("Index", "Home");
// }

// [HttpPost]
// public async Task<IActionResult> Login(string Username, string Heslo)
// {
//     // Validace vstupů
//     if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Heslo))
//     {
//         ModelState.AddModelError("", "Uživatelské jméno a heslo jsou povinné.");
//         return View("Index");
//     }

//     // Získání uživatele z databáze podle uživatelského jména
//     var uzivatel = await _uzivatelService.GetUzivatelByUsernameAsync(Username);
//     if (uzivatel == null)
//     {
//         ModelState.AddModelError("Username", "Tento uživatel neexistuje.");
//         //return View();
//         return View("Index");
//     }

//     // Porovnání zadaného hesla s hashovaným heslem z databáze
//     bool isPasswordValid = BCrypt.Net.BCrypt.Verify(Heslo, uzivatel.HESLO);
//     if (!isPasswordValid)
//     {
//         ModelState.AddModelError("Heslo", "Nesprávné heslo.");
//         //return View();
//         return View("Index");
//     }

//     // Přihlášení uživatele (uložení do session nebo cookies)
//     HttpContext.Session.SetString("UserId", uzivatel.ID_UZIVATEL.ToString());
//     HttpContext.Session.SetString("Role", uzivatel.ID_ROLE.ToString());

//     if (uzivatel.ID_ROLE == 1) // Admin role
//     {
//         return RedirectToAction("Admin", "Index");
//     }
//     // else if (uzivatel.ID_ROLE == 2) // Majitel - běžný uživatel
//     // {
//     //     return RedirectToAction("Majitel", "Index");
//     // }
//     //else if (uzivatel.ID_ROLE == 3) // Vedouce 
//     // {
//     //     return RedirectToAction("Vedouce", "Index");
//     // }
//     // else
//     // {
//     //     return RedirectToAction("Index", "Home"); // Výchozí přesměrování
//     // }


//     // Přesměrování na domovskou stránku nebo dashboard
//     return RedirectToAction("Index", "Home");
// }