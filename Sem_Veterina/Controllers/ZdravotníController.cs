using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

using Sem_Veterina.Models;
using Sem_Veterina.Entity;
using Sem_Veterina.CRUD; // Import modelu ZDRAVOTNIAKCE

namespace Sem_Veterina.Controllers
{
    public class ZDRAVOTNIAKCEController : Controller
    {
        private readonly ZdravotniAkceService _zdravotniAkceService;

        public ZDRAVOTNIAKCEController(ZdravotniAkceService zdravotniAkceService)
        {
            _zdravotniAkceService = zdravotniAkceService;
        }

        // GET: /ZDRAVOTNIAKCE/Index
        public async Task<IActionResult> Index()
        {
            List<ZDRAVOTNIAKCE> zdravotniAkce = await _zdravotniAkceService.GetAllZdravotniAkceAsync();
            return View(zdravotniAkce);
        }

        // GET: /ZDRAVOTNIAKCE/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var akce = await _zdravotniAkceService.GetZdravotniAkceByIdAsync(id);
            if (akce == null)
            {
                return NotFound();
            }
            return View(akce);
        }

        // GET: /ZDRAVOTNIAKCE/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /ZDRAVOTNIAKCE/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ZDRAVOTNIAKCE akce)
        {
            if (ModelState.IsValid)
            {
                await _zdravotniAkceService.AddZdravotniAkceAsync(akce);
                return RedirectToAction(nameof(Index));
            }
            return View(akce);
        }

        // GET: /ZDRAVOTNIAKCE/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var akce = await _zdravotniAkceService.GetZdravotniAkceByIdAsync(id);
            if (akce == null)
            {
                return NotFound();
            }
            return View(akce);
        }

        // POST: /ZDRAVOTNIAKCE/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ZDRAVOTNIAKCE akce)
        {
            if (id != akce.ID_AKCE)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _zdravotniAkceService.UpdateZdravotniAkceAsync(akce);
                return RedirectToAction(nameof(Index));
            }
            return View(akce);
        }

        // GET: /ZDRAVOTNIAKCE/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var akce = await _zdravotniAkceService.GetZdravotniAkceByIdAsync(id);
            if (akce == null)
            {
                return NotFound();
            }
            return View(akce);
        }

        // POST: /ZDRAVOTNIAKCE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _zdravotniAkceService.DeleteZdravotniAkceAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}