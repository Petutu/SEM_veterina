using Microsoft.AspNetCore.Mvc;

namespace Sem_Veterina.Controllers
{
    using global::Sem_Veterina.CRUD;
    using global::Sem_Veterina.Entity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class KlinikaController : ControllerBase
    {
        private readonly KlinikaService _klinikaService;

        public KlinikaController(KlinikaService klinikaService)
        {
            _klinikaService = klinikaService;
        }

        // READ - GET ALL
        [HttpGet]
        public async Task<IActionResult> GetAllKliniky()
        {
            var kliniky = await _klinikaService.GetAllKlinikyAsync();
            return Ok(kliniky);
        }

        // READ - GET BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetKlinikaById(int id)
        {
            var klinika = await _klinikaService.GetKlinikaByIdAsync(id);
            if (klinika == null)
                return NotFound();
            return Ok(klinika);
        }


        [HttpPost("CreateKlinika")]
        public async Task<IActionResult> CreateKlinika([FromBody] KLINIKY klinika)
        {
            if (klinika == null)
            {
                return BadRequest(new { Error = "Neplatná data pro vytvoření kliniky." });
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage)
                                              .ToList();
                return BadRequest(new { Error = "Neplatná data.", Details = errors });
            }

            try
            {
                // Vytvoříme instanci entity KLINIKY na základě ViewModelu
                var novaKlinika = new KLINIKY
                {
                    NÁZEV = klinika.NÁZEV,
                    ADRESA = klinika.ADRESA,
                    TELEFONNÍ_ČÍSLO = klinika.TELEFONNÍ_ČÍSLO,
                    EMAIL = klinika.EMAIL
                };

                // Voláme službu, která provede zápis do databáze
                await _klinikaService.AddKlinikaAsync(novaKlinika);

                return Ok(new { Message = "Klinika byla úspěšně vytvořena." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKlinika(int id)
        {
            try
            {
                await _klinikaService.DeleteKlinikaAsync(id);
                return Ok(new { Message = "Klinika byla úspěšně odstraněna." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

    }
}
