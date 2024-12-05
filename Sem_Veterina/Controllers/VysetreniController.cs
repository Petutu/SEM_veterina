using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Sem_Veterina.Entity;
using Sem_Veterina.CRUD;

namespace Sem_Veterina.Controllers
{
    public class VYSETRENIController : Controller
    {
        private readonly VysetreniService _vysetreniService;

        public VYSETRENIController(VysetreniService vysetreniService)
        {
            _vysetreniService = vysetreniService;
        }

        public async Task<IActionResult> Index()
        {
            List<VYSETRENI> vysetreni = await _vysetreniService.GetAllVysetreniAsync();
            return View(vysetreni);
        }

        public async Task<IActionResult> Details(int id)
        {
            var vys = await _vysetreniService.GetVysetreniByIdAsync(id);
            if (vys == null)
            {
                return NotFound();
            }
            return View(vys);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VYSETRENI vys)
        {
            if (ModelState.IsValid)
            {
                await _vysetreniService.AddVysetreniAsync(vys);
                return RedirectToAction(nameof(Index));
            }
            return View(vys);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var vys = await _vysetreniService.GetVysetreniByIdAsync(id);
            if (vys == null)
            {
                return NotFound();
            }
            return View(vys);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VYSETRENI vys)
        {
            if (id != vys.ID_AKCE)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _vysetreniService.UpdateVysetreniAsync(vys);
                return RedirectToAction(nameof(Index));
            }
            return View(vys);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var vys = await _vysetreniService.GetVysetreniByIdAsync(id);
            if (vys == null)
            {
                return NotFound();
            }
            return View(vys);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _vysetreniService.DeleteVysetreniAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
