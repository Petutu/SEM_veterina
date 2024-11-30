using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

using Sem_Veterina.Models;
using Sem_Veterina.Entity;
using Sem_Veterina.CRUD; // Import modelu ROLE

namespace Sem_Veterina.Controllers
{
    public class ROLEController : Controller
    {
        private readonly RoleService _roleService;

        public ROLEController(RoleService roleService)
        {
            _roleService = roleService;
        }

        // GET: /ROLE/Index
        public async Task<IActionResult> Index()
        {
            List<ROLE> role = await _roleService.GetAllRoleAsync();
            return View(role);
        }

        // GET: /ROLE/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        // GET: /ROLE/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /ROLE/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ROLE role)
        {
            if (ModelState.IsValid)
            {
                await _roleService.AddRoleAsync(role);
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        // GET: /ROLE/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        // POST: /ROLE/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ROLE role)
        {
            if (id != role.ID_ROLE)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _roleService.UpdateRoleAsync(role);
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        // GET: /ROLE/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        // POST: /ROLE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _roleService.DeleteRoleAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}