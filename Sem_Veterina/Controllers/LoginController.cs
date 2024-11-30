using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

using Sem_Veterina.Models;
using Sem_Veterina.Entity;
using Sem_Veterina.CRUD; // Import modelu LOGOVANI

namespace Sem_Veterina.Controllers
{
    public class LOGOVANIController : Controller
    {
        private readonly LogovaniService _logovaniService;

        public LOGOVANIController(LogovaniService logovaniService)
        {
            _logovaniService = logovaniService;
        }

        // GET: /LOGOVANI/Index
        public async Task<IActionResult> Index()
        {
            List<LOGOVANI> logy = await _logovaniService.GetAllLogsAsync();
            return View(logy);
        }

        // GET: /LOGOVANI/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var log = await _logovaniService.GetLogByIdAsync(id);
            if (log == null)
            {
                return NotFound();
            }
            return View(log);
        }

        // GET: /LOGOVANI/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /LOGOVANI/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LOGOVANI log)
        {
            if (ModelState.IsValid)
            {
                await _logovaniService.AddLogAsync(log);
                return RedirectToAction(nameof(Index));
            }
            return View(log);
        }

        // GET: /LOGOVANI/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var log = await _logovaniService.GetLogByIdAsync(id);
            if (log == null)
            {
                return NotFound();
            }
            return View(log);
        }

        // POST: /LOGOVANI/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LOGOVANI log)
        {
            if (id != log.ID_LOGOVANI)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _logovaniService.UpdateLogAsync(log);
                return RedirectToAction(nameof(Index));
            }
            return View(log);
        }

        // GET: /LOGOVANI/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var log = await _logovaniService.GetLogByIdAsync(id);
            if (log == null)
            {
                return NotFound();
            }
            return View(log);
        }

        // POST: /LOGOVANI/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _logovaniService.DeleteLogAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}