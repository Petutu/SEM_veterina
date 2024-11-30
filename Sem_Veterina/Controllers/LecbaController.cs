using Microsoft.AspNetCore.Mvc;

namespace Sem_Veterina.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using global::Sem_Veterina.Entity;

    namespace Sem_Veterina.Controllers
    {
        public class LECBYController : Controller
        {
            private readonly LecbaService _lecbyService;

            public LECBYController(LecbaService lecbyService)
            {
                _lecbyService = lecbyService;
            }

            // GET: /LECBY/Index
            public async Task<IActionResult> Index()
            {
                List<LECBY> lecby = await _lecbyService.GetAllLecbyAsync();
                return View(lecby);
            }

            // GET: /LECBY/Details/5
            public async Task<IActionResult> Details(int id)
            {
                var lecba = await _lecbyService.GetLecbaByIdAsync(id);
                if (lecba == null)
                {
                    return NotFound();
                }
                return View(lecba);
            }

            // GET: /LECBY/Create
            public IActionResult Create()
            {
                return View();
            }

            // POST: /LECBY/Create
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(LECBY lecba)
            {
                if (ModelState.IsValid)
                {
                    await _lecbyService.AddLecbaAsync(lecba);
                    return RedirectToAction(nameof(Index));
                }
                return View(lecba);
            }

            // GET: /LECBY/Edit/5
            public async Task<IActionResult> Edit(int id)
            {
                var lecba = await _lecbyService.GetLecbaByIdAsync(id);
                if (lecba == null)
                {
                    return NotFound();
                }
                return View(lecba);
            }

            // POST: /LECBY/Edit/5
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, LECBY lecba)
            {
                if (id != lecba.ID_LÉČBA)
                {
                    return BadRequest();
                }

                if (ModelState.IsValid)
                {
                    await _lecbyService.UpdateLecbaAsync(lecba);
                    return RedirectToAction(nameof(Index));
                }
                return View(lecba);
            }

            // GET: /LECBY/Delete/5
            public async Task<IActionResult> Delete(int id)
            {
                var lecba = await _lecbyService.GetLecbaByIdAsync(id);
                if (lecba == null)
                {
                    return NotFound();
                }
                return View(lecba);
            }

            // POST: /LECBY/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                await _lecbyService.DeleteLecbaAsync(id);
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
