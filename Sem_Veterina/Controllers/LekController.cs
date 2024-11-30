using Microsoft.AspNetCore.Mvc;

namespace Sem_Veterina.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using System.Collections.Generic;


    using global::Sem_Veterina.Entity;

    namespace Sem_Veterina.Controllers
    {
        public class LEKYController : Controller
        {
            private readonly LekyService _lekyService;

            public LEKYController(LekyService lekyService)
            {
                _lekyService = lekyService;
            }

            // GET: /LEKY/Index
            public async Task<IActionResult> Index()
            {
                List<LEKY> leky = await _lekyService.GetAllLekyAsync();
                return View(leky);
            }

            // GET: /LEKY/Details/5
            public async Task<IActionResult> Details(int id)
            {
                var lek = await _lekyService.GetLekByIdAsync(id);
                if (lek == null)
                {
                    return NotFound();
                }
                return View(lek);
            }

            // GET: /LEKY/Create
            public IActionResult Create()
            {
                return View();
            }

            // POST: /LEKY/Create
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(LEKY lek)
            {
                if (ModelState.IsValid)
                {
                    await _lekyService.AddLekAsync(lek);
                    return RedirectToAction(nameof(Index));
                }
                return View(lek);
            }

            // GET: /LEKY/Edit/5
            public async Task<IActionResult> Edit(int id)
            {
                var lek = await _lekyService.GetLekByIdAsync(id);
                if (lek == null)
                {
                    return NotFound();
                }
                return View(lek);
            }

            // POST: /LEKY/Edit/5
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, LEKY lek)
            {
                if (id != lek.ID_LÉK)
                {
                    return BadRequest();
                }

                if (ModelState.IsValid)
                {
                    await _lekyService.UpdateLekAsync(lek);
                    return RedirectToAction(nameof(Index));
                }
                return View(lek);
            }

            // GET: /LEKY/Delete/5
            public async Task<IActionResult> Delete(int id)
            {
                var lek = await _lekyService.GetLekByIdAsync(id);
                if (lek == null)
                {
                    return NotFound();
                }
                return View(lek);
            }

            // POST: /LEKY/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                await _lekyService.DeleteLekAsync(id);
                return RedirectToAction(nameof(Index));
            }
        }
    }
