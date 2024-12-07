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
        private readonly DiagnozaService _diagnozaService;
        private readonly LogovaniService _logovaniService;
        private readonly UzivatelService _uzivatelService;

        public AdminController(
            KlinikaService klinikaService,
            MajitelService majitelService,
            ZvireService zvireService,
            ZdravotniAkceService zdravotniAkceService,
            PristrojeService pristrojeService,
            LecbaService lecbaService,
            LekyService lekyService,
            PersonalService personalService,
            DiagnozaService diagnozaService,
            LogovaniService logovaniService,
            UzivatelService uzivatelService
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
            _diagnozaService = diagnozaService;
            _logovaniService = logovaniService;
            _uzivatelService = uzivatelService;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Akce pro stránku "Kliniky"`
        public async Task<IActionResult> Kliniky(string? address, string? phone)
        {
            var viewModel = new KlinikyViewModel();
            viewModel.Kliniky = await _klinikaService.GetFilteredKlinikyAsync(address, phone);
            viewModel.SelectedKlinika = viewModel.Kliniky.FirstOrDefault();
            return View("Kliniky", viewModel);  // Vrátí seznam klinik do pohledu Kliniky.cshtml
        }

        // Akce pro stránku "Majitele"
        public async Task<IActionResult> Majitele(string? name, string? lastname, string? phone)
        {
            var viewModel = new MajiteleViewModel();
            viewModel.Majitele = await _majitelService.GetFilteredMajiteleAsync(name, lastname, phone);
            return View("Majitele", viewModel);  // Vrátí seznam majitel do pohledu Kliniky.cshtml
        }
        // Akce pro stránku "Malí pacienty"
        public async Task<IActionResult> Zvirata(string? name, string? species)
        {
            var viewModel = new ZvirataViewModel();
            viewModel.Zvirata = await _zvireService.GetFilteredZvirataAsync(name, species);
            viewModel.SelectedZvire = viewModel.Zvirata.FirstOrDefault();
            // Debug.WriteLine($"Zvirata count: {viewModel.Zvirata?.Count}");
            // Debug.WriteLine($"First Zvire name: {viewModel.Zvirata?.FirstOrDefault()?.JMÉNO_ZVÍŘETE}");
            //return View("~/Views/Admin/Zvirata.cshtml", viewModel);
            return View("Zvirata", viewModel);  // Vrátí seznam zvířat do pohledu Zvirata.cshtml
        }
        // Akce pro stránku "Diagnózy"
        public async Task<IActionResult> Diagnozy(string? name, string? description)
        {
            var viewModel = new DiagnozyViewModel();
            viewModel.Diagnozy = await _diagnozaService.GetFilteredDiagnózyAsync(name, description);
            viewModel.SelectedDiagnoza = viewModel.Diagnozy.FirstOrDefault();
            return View("Diagnozy", viewModel);  // Vrátí seznam diagnóz do pohledu Diagnozy.cshtml
        }
        // Akce pro stránku "Léčby"
        public async Task<IActionResult> Lecby(string? description)
        {
            var viewModel = new LecbyViewModel();
            viewModel.Lecby = await _lecbaService.GetFilteredLecbyAsync(description);
            viewModel.SelectedLecba = viewModel.Lecby.FirstOrDefault();
            return View("Lecby", viewModel);  // Vrátí seznam léčeb do pohledu Lecby.cshtml
        }
        // Akce pro stránku "Léky"
        public async Task<IActionResult> Leky(string? name, string? instructions)
        {
            var viewModel = new LekyViewModel();
            viewModel.Leky = await _lekyService.GetFilteredLekyAsync(name, instructions);
            viewModel.SelectedLek = viewModel.Leky.FirstOrDefault();
            return View("Leky", viewModel);  // Vrátí seznam léků do pohledu Leky.cshtml
        }
        // Akce pro stránku "Personál"
        public async Task<IActionResult> Personal(string? name, string? surname, string? specialization, int? klinikaId)
        {
            var viewModel = new PersonalViewModel();
            viewModel.Personal = await _personalService.GetFilteredPersonalAsync(name, surname, specialization, klinikaId);
            viewModel.SelectedPersonal = viewModel.Personal.FirstOrDefault();
            return View("Personal", viewModel);  // Vrátí seznam personálu do pohledu Personal.cshtml
        }

        // Akce pro stránku "Přístroje"
        public async Task<IActionResult> Pristroje(string? name, string? function, int? klinikaId)
        {
            var viewModel = new PristrojeViewModel();
            viewModel.Pristroje = await _pristrojeService.GetFilteredPristrojeAsync(name, function, klinikaId);
            viewModel.SelectedPristroj = viewModel.Pristroje.FirstOrDefault();
            return View("Pristroje", viewModel);  // Vrátí seznam přístrojů do pohledu Pristroje.cshtml
        }
        // Akce pro stránku "Uživatelé"
        public async Task<IActionResult> Uzivatele(string? username, string? roleName)
        {
            var viewModel = new UzivateleViewModel();
            viewModel.Uzivatele = await _uzivatelService.GetFilteredUzivateleAsync(username, roleName);
            viewModel.SelectedUzivatel = viewModel.Uzivatele.FirstOrDefault();
            return View("Uzivatele", viewModel);  // Vrátí seznam uživatelů do pohledu Uzivatele.cshtml
        }
        // Akce pro stránku "Historie"
        public async Task<IActionResult> Logovani(string? tableName, string? operation)
        {
            var viewModel = new LogovaniViewModel();
            viewModel.Logovani = await _logovaniService.GetFilteredLogovaniAsync(tableName, operation);
            viewModel.SelectedLog = viewModel.Logovani.FirstOrDefault();
            return View("Logovani", viewModel);  // Vrátí seznam logů do pohledu Logovani.cshtml
        }
        // Akce pro stránku "Systémný katalog"

    }
}