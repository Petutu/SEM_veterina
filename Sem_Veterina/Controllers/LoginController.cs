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
            ModelState.Clear();
            // Zajištění výchozího stavu
            ViewBag.RegisterModal = false;
            ViewBag.LoginModal = false;
            var model = new CombinedViewModel
            {
                Login = new LoginViewModel(),
                Register = new RegisterViewModel()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            // Ladící výstupy
            Console.WriteLine("Data dorazila na server:");
            Console.WriteLine($"Username: {model.RegisterUsername}, Password: {model.RegisterPassword}");

            if (!ModelState.IsValid)
            {
                ViewBag.RegisterModal = true;
                return View("Index", new CombinedViewModel { Register = model });
            }

            // Validace vstupů
            if (string.IsNullOrWhiteSpace(model.RegisterUsername) || string.IsNullOrWhiteSpace(model.RegisterPassword))
            {
                ModelState.AddModelError("", "Uživatelské jméno a heslo jsou povinné.");
                ViewBag.RegisterModal = true;
                return View("Index", new CombinedViewModel { Register = model });
            }

            // Kontrola, zda uživatel již existuje
            var existujiciUzivatel = await _uzivatelService.GetUzivatelByUsernameAsync(model.RegisterUsername);
            if (existujiciUzivatel != null)
            {
                ModelState.AddModelError("Register.RegisterUsername", "Uživatel s tímto jménem již existuje.");
                ViewBag.RegisterModal = true;
                return View("Index", new CombinedViewModel { Register = model });
            }

            // Vytvoření nového uživatele
            var novyUzivatel = new UZIVATEL
            {
                USERNAME = model.RegisterUsername,
                HESLO = BCrypt.Net.BCrypt.HashPassword(model.RegisterPassword), // Použijte šifrování hesla
                ID_ROLE = 2
            };
            await _uzivatelService.AddUzivatelAsync(novyUzivatel);

            // Přesměrování na přihlašovací stránku
            TempData["SuccessMessage"] = "Účet byl úspěšně vytvořen. Nyní se můžete přihlásit.";
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            // Validace vstupů
            if (!ModelState.IsValid)
            {
                ViewBag.LoginModal = true;
                return View("Index", new CombinedViewModel { Login = model });

            }

            // Validace vstupů
            if (string.IsNullOrWhiteSpace(model.LoginUsername))
            {
                ModelState.AddModelError("Login.LoginUsername", "Uživatelské jméno a heslo jsou povinné.");
                ViewBag.RegisterModal = true;
                return View("Index", new CombinedViewModel { Login = model });
            }

            // Získání uživatele z databáze
            var uzivatel = await _uzivatelService.GetUzivatelByUsernameAsync(model.LoginUsername);
            if (uzivatel == null)
            {
                ModelState.AddModelError("Login.LoginUsername", "Tento uživatel neexistuje.");
                ViewBag.LoginModal = true;
                return View("Index", new CombinedViewModel { Login = model });
            }

            // Kontrola hesla
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(model.LoginPassword, uzivatel.HESLO);
            if (!isPasswordValid)
            {
                ModelState.AddModelError("Login.LoginPassword", "Nesprávné heslo.");
                ViewBag.LoginModal = true;
                return View("Index", new CombinedViewModel { Login = model });
            }

            // // Přihlášení - bude chyba
            // HttpContext.Session.SetString("UserId", uzivatel.ID_UZIVATEL.ToString());
            // HttpContext.Session.SetString("Role", uzivatel.ID_ROLE.ToString());

            // if (uzivatel.ID_ROLE == 1) // Admin role
            // {
            //     return RedirectToAction("Index", "Admin");
            // }
            // else if (uzivatel.ID_ROLE == 2) // Majitel - běžný uživatel
            // {
            //     return RedirectToAction("Index", "Majitel");
            // }
            //else if (uzivatel.ID_ROLE == 3) // Vedouce 
            // {
            //     return RedirectToAction("Vedouce", "Index");
            // }
            // else
            // {
            //     return RedirectToAction("Index", "Home"); // Výchozí přesměrování
            // }

            switch (uzivatel.ID_ROLE)
            {
                case 1: // Admin
                    return RedirectToAction("Index", "Admin");
                case 2: // Běžný uživatel
                    return RedirectToAction("Index", "Majitel");
                // case 3: //Vedouci
                //     return RedirectToAction("Index", "Vedouci");
                // case 4: //Podřízený
                //     return RedirectToAction("Index", "Personal");
                default:
                    return RedirectToAction("Index", "Home");
            }


            // // Přesměrování na domovskou stránku nebo dashboard
            // return RedirectToAction("Index", "Home");
        }
    }
}