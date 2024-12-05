using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Sem_Veterina.Entity;
using Sem_Veterina.CRUD;

namespace Sem_Veterina.Controllers
{
    public class VYSLEDKYKRVEController : Controller
    {
        private readonly VysledkyKrveService _vysledkyKrveService;

        public VYSLEDKYKRVEController(VysledkyKrveService vysledkyKrveService)
        {
            _vysledkyKrveService = vysledkyKrveService;
        }

        public async Task<IActionResult> Index()
        {
            List<VYSLEDKYKRVE> vysledky = await _vysledkyKrveService.GetAllVysledkyKrveAsync();
            return View(vysledky);
        }

        public async Task<IActionResult> Details(int id)
        {
            var vysledek = await _vysledkyKrveService.GetVysledekKrveByIdAsync(id);
            if (vysledek == null)
            {
                return NotFound();
            }
            return View(vysledek);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VYSLEDKYKRVE vysledek)
        {
            if (ModelState.IsValid)
            {
                await _vysledkyKrveService.AddVysledekKrveAsync(vysledek);
                return RedirectToAction(nameof(Index));
            }
            return View(vysledek);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var vysledek = await _vysledkyKrveService.GetVysledekKrveByIdAsync(id);
            if (vysledek == null)
            {
                return NotFound();
            }
            return View(vysledek);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VYSLEDKYKRVE vysledek)
        {
            if (id != vysledek.ID_AKCE)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _vysledkyKrveService.UpdateVysledekKrveAsync(vysledek);
                return RedirectToAction(nameof(Index));
            }
            return View(vysledek);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var vysledek = await _vysledkyKrveService.GetVysledekKrveByIdAsync(id);
            if (vysledek == null)
            {
                return NotFound();
            }
            return View(vysledek);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _vysledkyKrveService.DeleteVysledekKrveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
