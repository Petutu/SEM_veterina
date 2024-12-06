using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

using Sem_Veterina.Models;
using Sem_Veterina.Entity;
using Sem_Veterina.CRUD; // Import modelu UZIVATEL

namespace Sem_Veterina.Controllers
{
    public class UZIVATELController : Controller
    {
        private readonly UzivatelService _uzivatelService;

        public UZIVATELController(UzivatelService uzivatelService)
        {
            _uzivatelService = uzivatelService;
        }

        // GET: /UZIVATEL/Index
        public async Task<IActionResult> Index()
        {
            List<UZIVATEL> uzivatele = await _uzivatelService.GetAllUzivateleAsync();
            return View(uzivatele);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUzerById(int id)
        {
            var uzivatel = await _uzivatelService.GetUzivatelByIdAsync(id);
            if (uzivatel == null)
            {
                return NotFound();
            }
            return Ok(uzivatel);
            //return View(uzivatel);
        }

        // // GET: /UZIVATEL/Create
        // public IActionResult Create()
        // {
        //     return View();
        // }

        // POST: /UZIVATEL/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UZIVATEL uzivatel)
        {
            if (ModelState.IsValid)
            {
                await _uzivatelService.AddUzivatelAsync(uzivatel);
                return RedirectToAction(nameof(Index));
            }
            return View(uzivatel);
        }

        // GET: /UZIVATEL/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var uzivatel = await _uzivatelService.GetUzivatelByIdAsync(id);
            if (uzivatel == null)
            {
                return NotFound();
            }
            return View(uzivatel);
        }

        // POST: /UZIVATEL/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UZIVATEL uzivatel)
        {
            if (id != uzivatel.ID_UZIVATEL)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _uzivatelService.UpdateUzivatelAsync(uzivatel);
                return RedirectToAction(nameof(Index));
            }
            return View(uzivatel);
        }

        // GET: /UZIVATEL/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var uzivatel = await _uzivatelService.GetUzivatelByIdAsync(id);
            if (uzivatel == null)
            {
                return NotFound();
            }
            return View(uzivatel);
        }

        // POST: /UZIVATEL/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _uzivatelService.DeleteUzivatelAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
