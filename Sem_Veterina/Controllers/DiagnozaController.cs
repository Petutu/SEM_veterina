namespace Sem_Veterina.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using global::Sem_Veterina.Entity;
    using Microsoft.AspNetCore.Mvc;

    namespace Sem_Veterina.Controllers
    {
        public class DIAGNOZYController : Controller
        {
            private readonly DiagnozaService _diagnózyService;

            public DIAGNOZYController(DiagnozaService diagnózyService)
            {
                _diagnózyService = diagnózyService;
            }

            // GET: /DIAGNOZY/Index
            public async Task<IActionResult> Index()
            {
                List<DIAGNOZY> diagnózy = await _diagnózyService.GetAllDiagnozyAsync();
                return View(diagnózy);
            }

            // GET: /DIAGNOZY/Details/5
            public async Task<IActionResult> Details(int id)
            {
                var diagnóza = await _diagnózyService.GetDiagnozaByIdAsync(id);
                if (diagnóza == null)
                {
                    return NotFound();
                }
                return View(diagnóza);
            }

            // GET: /DIAGNOZY/Create
            public IActionResult Create()
            {
                return View();
            }

            // POST: /DIAGNOZY/Create
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(DIAGNOZY diagnóza)
            {
                if (ModelState.IsValid)
                {
                    await _diagnózyService.AddDiagnozaAsync(diagnóza);
                    return RedirectToAction(nameof(Index));
                }
                return View(diagnóza);
            }

            // GET: /DIAGNOZY/Edit/5
            public async Task<IActionResult> Edit(int id)
            {
                var diagnóza = await _diagnózyService.GetDiagnozaByIdAsync(id);
                if (diagnóza == null)
                {
                    return NotFound();
                }
                return View(diagnóza);
            }

            // POST: /DIAGNOZY/Edit/5
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, DIAGNOZY diagnóza)
            {
                if (id != diagnóza.ID_DIAGNÓZA)
                {
                    return BadRequest();
                }

                if (ModelState.IsValid)
                {
                    await _diagnózyService.UpdateDiagnozaAsync(diagnóza);
                    return RedirectToAction(nameof(Index));
                }
                return View(diagnóza);
            }

            // GET: /DIAGNOZY/Delete/5
            public async Task<IActionResult> Delete(int id)
            {
                var diagnóza = await _diagnózyService.GetDiagnozaByIdAsync(id);
                if (diagnóza == null)
                {
                    return NotFound();
                }
                return View(diagnóza);
            }

            // POST: /DIAGNOZY/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                await _diagnózyService.DeleteDiagnozaAsync(id);
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
