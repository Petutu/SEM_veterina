using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

using Sem_Veterina.Models;
using Sem_Veterina.Entity;
using Sem_Veterina.CRUD; // Import modelu LOGOVANI

namespace Sem_Veterina.Controllers
{
    public class LogovaniController : Controller
    {
        private readonly LogovaniService _logovaniService;

        public LogovaniController(
            LogovaniService logovaniService
        )
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
    }
}