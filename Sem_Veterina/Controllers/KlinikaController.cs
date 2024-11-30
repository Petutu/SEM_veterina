using Microsoft.AspNetCore.Mvc;

namespace Sem_Veterina.Controllers
{
    using global::Sem_Veterina.CRUD;
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

    }
}
