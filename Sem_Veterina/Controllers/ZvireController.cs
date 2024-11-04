using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

using Sem_Veterina.Models;
using Sem_Veterina.Entity;
using Sem_Veterina.CRUD;  // Import modelu ZVIRATA

namespace Sem_Veterina.Controllers
{
    public class ZVIRATAController : Controller
    {
        private readonly ZvireService _zvireService;

        public ZVIRATAController(ZvireService zvireService)
        {
            _zvireService = zvireService;
        }

        // GET: /ZVIRATA/Index
        public async Task<IActionResult> Index()
        {
            List<ZVIRATA> zvirata = await _zvireService.GetAllZvirataAsync();
            return View(zvirata);
        }

        // GET: /ZVIRATA/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var zvire = await _zvireService.GetZvireByIdAsync(id);
            if (zvire == null)
            {
                return NotFound();
            }
            return View(zvire);
        }

        // GET: /ZVIRATA/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /ZVIRATA/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ZVIRATA zvire)
        {
            if (ModelState.IsValid)
            {
                await _zvireService.AddZvireAsync(zvire);
                return RedirectToAction(nameof(Index));
            }
            return View(zvire);
        }

        // GET: /ZVIRATA/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var zvire = await _zvireService.GetZvireByIdAsync(id);
            if (zvire == null)
            {
                return NotFound();
            }
            return View(zvire);
        }

        // POST: /ZVIRATA/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ZVIRATA zvire)
        {
            if (id != zvire.ID_ZVÍŘE)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _zvireService.UpdateZvireAsync(zvire);
                return RedirectToAction(nameof(Index));
            }
            return View(zvire);
        }

        // GET: /ZVIRATA/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var zvire = await _zvireService.GetZvireByIdAsync(id);
            if (zvire == null)
            {
                return NotFound();
            }
            return View(zvire);
        }

        // POST: /ZVIRATA/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _zvireService.DeleteZvireAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}