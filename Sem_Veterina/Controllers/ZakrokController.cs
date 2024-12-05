using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Sem_Veterina.Entity;
using Sem_Veterina.CRUD;

namespace Sem_Veterina.Controllers
{
    public class ZAKROKController : Controller
    {
        private readonly ZakrokService _zakrokService;

        public ZAKROKController(ZakrokService zakrokService)
        {
            _zakrokService = zakrokService;
        }

        public async Task<IActionResult> Index()
        {
            List<ZAKROK> zakroky = await _zakrokService.GetAllZakrokyAsync();
            return View(zakroky);
        }

        public async Task<IActionResult> Details(int id)
        {
            var zakrok = await _zakrokService.GetZakrokByIdAsync(id);
            if (zakrok == null)
            {
                return NotFound();
            }
            return View(zakrok);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ZAKROK zakrok)
        {
            if (ModelState.IsValid)
            {
                await _zakrokService.AddZakrokAsync(zakrok);
                return RedirectToAction(nameof(Index));
            }
            return View(zakrok);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var zakrok = await _zakrokService.GetZakrokByIdAsync(id);
            if (zakrok == null)
            {
                return NotFound();
            }
            return View(zakrok);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ZAKROK zakrok)
        {
            if (id != zakrok.ID_AKCE)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _zakrokService.UpdateZakrokAsync(zakrok);
                return RedirectToAction(nameof(Index));
            }
            return View(zakrok);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var zakrok = await _zakrokService.GetZakrokByIdAsync(id);
            if (zakrok == null)
            {
                return NotFound();
            }
            return View(zakrok);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _zakrokService.DeleteZakrokAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
