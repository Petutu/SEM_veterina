using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Sem_Veterina.CRUD;
using Sem_Veterina.Entity;
using Sem_Veterina.Models;

namespace Sem_Veterina.Controllers
{
    public class HomeController : Controller
    {
        private readonly KlinikaService _klinikaService;
        private readonly MajitelService _majitelService;
        private readonly ZvireService _zvireService;
        private readonly ZdravotniAkceService _zdravotniAkceService;
        private readonly PristrojeService _pristrojeService;
        private readonly LecbaService _lecbaService;
        private readonly LekyService _lekyService;
        private readonly PersonalService _personalService;

        public HomeController(
            KlinikaService klinikaService,
            MajitelService majitelService,
            ZvireService zvireService,
            ZdravotniAkceService zdravotniAkceService,
            PristrojeService pristrojeService,
            LecbaService lecbaService,
            LekyService lekyService,
            PersonalService personalService)
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
            var viewModel = new EntityViewModel
            {
                AvailableEntities = new List<string> { "Klinika", "Majitel", "Zvire", "Zdravotni Akce", "Pristroj", "Lecby", "Leky", "Personal" },
                SelectedEntity = "Klinika" // Výchozí hodnota
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> LoadEntity(string selectedEntity)
        {
            var viewModel = new EntityViewModel
            {
                AvailableEntities = new List<string> { "Klinika", "Majitel", "Zvire", "Zdravotni Akce", "Pristroj", "Lecby", "Leky", "Personal" }
            };

            switch (selectedEntity)
            {
                case "Klinika":
                    viewModel.Kliniky = await _klinikaService.GetAllKlinikyAsync();
                    break;
                case "Majitel":
                    viewModel.Majitele = await _majitelService.GetAllMajiteleAsync();
                    break;
                case "Zvire":
                    viewModel.Zvirata = await _zvireService.GetAllZvirataAsync();
                    break;
                case "Zdravotni Akce":
                    viewModel.ZdravotniAkce = await _zdravotniAkceService.GetAllZdravotniAkceAsync();
                    break;
                case "Pristroj":
                    viewModel.Pristroje = await _pristrojeService.GetAllPristrojeAsync();
                    break;
                case "Lecby":
                    viewModel.Lecby = await _lecbaService.GetAllLecbyAsync();
                    break;
                case "Leky":
                    viewModel.Leky = await _lekyService.GetAllLekyAsync();
                    break;
                case "Personal":
                    viewModel.Personal = await _personalService.GetAllPersonalAsync();
                    break;
            }

            viewModel.SelectedEntity = selectedEntity;
            return View("Index", viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }


}
