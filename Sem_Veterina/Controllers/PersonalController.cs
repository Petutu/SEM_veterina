using Microsoft.AspNetCore.Mvc;

namespace Sem_Veterina.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using System.Collections.Generic;

 
    using global::Sem_Veterina.CRUD;
    using global::Sem_Veterina.Entity;

    namespace Sem_Veterina.Controllers
    {
        public class PERSONALController : Controller
        {
            private readonly PersonalService _personalService;

            public PERSONALController(PersonalService personalService)
            {
                _personalService = personalService;
            }

            // GET: /PERSONAL/Index
            public async Task<IActionResult> Index()
            {
                List<PERSONAL> personal = await _personalService.GetAllPersonalAsync();
                return View(personal);
            }

            // GET: /PERSONAL/Details/5
            public async Task<IActionResult> Details(int id)
            {
                var personal = await _personalService.GetPersonalByIdAsync(id);
                if (personal == null)
                {
                    return NotFound();
                }
                return View(personal);
            }

            // GET: /PERSONAL/Create
            public IActionResult Create()
            {
                return View();
            }

            // POST: /PERSONAL/Create
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(PERSONAL personal)
            {
                if (ModelState.IsValid)
                {
                    await _personalService.AddPersonalAsync(personal);
                    return RedirectToAction(nameof(Index));
                }
                return View(personal);
            }

            // GET: /PERSONAL/Edit/5
            public async Task<IActionResult> Edit(int id)
            {
                var personal = await _personalService.GetPersonalByIdAsync(id);
                if (personal == null)
                {
                    return NotFound();
                }
                return View(personal);
            }

            // POST: /PERSONAL/Edit/5
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, PERSONAL personal)
            {
                if (id != personal.ID_PRESONÁL)
                {
                    return BadRequest();
                }

                if (ModelState.IsValid)
                {
                    await _personalService.UpdatePersonalAsync(personal);
                    return RedirectToAction(nameof(Index));
                }
                return View(personal);
            }

            // GET: /PERSONAL/Delete/5
            public async Task<IActionResult> Delete(int id)
            {
                var personal = await _personalService.GetPersonalByIdAsync(id);
                if (personal == null)
                {
                    return NotFound();
                }
                return View(personal);
            }

            // POST: /PERSONAL/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                await _personalService.DeletePersonalAsync(id);
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
