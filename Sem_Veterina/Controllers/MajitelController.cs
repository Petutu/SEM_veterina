using Microsoft.AspNetCore.Mvc;

namespace Sem_Veterina.Controllers
{
    using global::Sem_Veterina.CRUD;
    using global::Sem_Veterina.Entity;
    using Microsoft.AspNetCore.Mvc;

    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class MajitelController : Controller
    {
        private readonly MajitelService _majitelService;

        public MajitelController(MajitelService majitelService)
        {
            _majitelService = majitelService;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Zobrazení seznamu majitelů (hlavní stránka)
        // public async Task<IActionResult> Majitele()
        // {
        //     var majitele = await _majitelService.GetAllMajiteleAsync();
        //     return View(majitele);
        // }

        // Vytvoření nového majitele (při odeslání dat z pravého panelu)

        // READ - GET BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetKlinikaById(int id)
        {
            var majitel = await _majitelService.GetMajitelByIdAsync(id);
            if (majitel == null)
                return NotFound();
            return Ok(majitel);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MAJITELE majitel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                                   .Select(e => e.ErrorMessage)
                                                   .ToList();
                    return BadRequest(new { message = "Zadání dat není platné.", errors });
                }

                // Přidání majitele
                await _majitelService.AddMajitelAsync(majitel);
                return Ok(new { message = "Majitel byl úspěšně vytvořen!" });
            }
            catch (Exception ex)
            {
                // Odeslání chyby jako JSON
                return BadRequest(new { message = "Došlo k chybě při vytváření majitele.", error = ex.Message });
            }
        }

        // Editace existujícího majitele (při odeslání dat z pravého panelu)
        [HttpPost]
        public async Task<IActionResult> Edit(MAJITELE majitel)
        {
            if (ModelState.IsValid)
            {
                await _majitelService.UpdateMajitelAsync(majitel);
                //return RedirectToAction(nameof(Majitele));
            }
            return View("Majitele", await _majitelService.GetAllMajiteleAsync());
        }

        // Smazání majitele
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _majitelService.DeleteMajitelAsync(id);
            //return RedirectToAction(nameof(Majitele));
            return View();
        }
    }
}
