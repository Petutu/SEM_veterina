using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Sem_Veterina.CRUD;
using Sem_Veterina.Entity;
using Sem_Veterina.Models;

namespace Sem_Veterina.Controllers
{

    public class AdminController : Controller
    {
        private readonly KlinikaService _klinikaService;
        private readonly MajitelService _majitelService;
        private readonly ZvireService _zvireService;
        private readonly ZdravotniAkceService _zdravotniAkceService;
        private readonly PristrojeService _pristrojeService;
        private readonly LecbaService _lecbaService;
        private readonly LekyService _lekyService;
        private readonly PersonalService _personalService;

        public AdminController(
            KlinikaService klinikaService,
            MajitelService majitelService,
            ZvireService zvireService,
            ZdravotniAkceService zdravotniAkceService,
            PristrojeService pristrojeService,
            LecbaService lecbaService,
            LekyService lekyService,
            PersonalService personalService
        )
        {
            _klinikaService = klinikaService;
            _majitelService = majitelService;
            _zvireService = zvireService;
            _zdravotniAkceService = zdravotniAkceService;
            _pristrojeService = pristrojeService;
            _lecbaService = lecbaService;
            _lekyService = lekyService;
            _personalService = personalService;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Akce pro stránku "Kliniky"
        public async Task<IActionResult> Kliniky()
        {
            var viewModel = new AdminViewModel
            {

            };
            viewModel.Kliniky = await _klinikaService.GetAllKlinikyAsync();
            return View("Kliniky", viewModel);  // Vrátí seznam klinik do pohledu Kliniky.cshtml
        }

        // Akce pro zobrazení detailů kliniky (například po kliknutí na výpisu vlevo)
        public async Task<IActionResult> KlinikaDetail(int id)
        {
            var klinika = await _klinikaService.GetKlinikaByIdAsync(id);
            return PartialView("_KlinikaDetail", klinika);  // Vrátí detaily vybrané kliniky
        }

    }
}