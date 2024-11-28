using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

using Sem_Veterina.Models;
using Sem_Veterina.Entity;
using Sem_Veterina.CRUD; // Import modelu LOGOVANI

namespace Sem_Veterina.Controllers
{
    public class LOGOVANIController : Controller
    {
        private readonly LogovaniService _logovaniService;

        public LOGOVANIController(LogovaniService logovaniService)
        {
            _logovaniService = logovaniService;
        }

        // GET: /LOGOVANI/Index
        public async Task<IActionResult> Index()
        {
            // var viewModel = new LoginViewModel
            // {
            // };
            // return View(viewModel);
            return View();
        }

        // GET: /LOGOVANI/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /LOGOVANI/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LOGOVANI log)
        {
            if (ModelState.IsValid)
            {
                await _logovaniService.AddLogAsync(log);
                return RedirectToAction(nameof(Index));
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

        // GET: /LOGOVANI/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var log = await _logovaniService.GetLogByIdAsync(id);
            if (log == null)
            {
                return NotFound();
            }
            return View(log);
        }

        // POST: /LOGOVANI/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LOGOVANI log)
        {
            if (id != log.ID_LOGOVANI)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
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

        // GET: /LOGOVANI/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var log = await _logovaniService.GetLogByIdAsync(id);
            if (log == null)
            {
                return NotFound();
            }
            return View(log);
        }

        // POST: /LOGOVANI/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _logovaniService.DeleteLogAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}