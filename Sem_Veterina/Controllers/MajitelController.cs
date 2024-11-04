using Microsoft.AspNetCore.Mvc;

namespace Sem_Veterina.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Sem_Veterina.CRUD;
    using Sem_Veterina.Entity;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class MajitelController : ControllerBase
    {
        private readonly MajitelService _majitelService;

        public MajitelController(MajitelService majitelService)
        {
            _majitelService = majitelService;
        }

        // CREATE
        [HttpPost]
        public async Task<IActionResult> CreateMajitel([FromBody] MAJITELE majitel)
        {
            if (majitel == null)
                return BadRequest();

            await _majitelService.AddMajitelAsync(majitel);
            return Ok(majitel);
        }

        // READ - GET ALL
        [HttpGet]
        public async Task<IActionResult> GetAllMajitele()
        {
            var majitele = await _majitelService.GetAllMajiteleAsync();
            return Ok(majitele);
        }

        // READ - GET BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMajitelById(int id)
        {
            var majitel = await _majitelService.GetMajitelByIdAsync(id);
            if (majitel == null)
                return NotFound();
            return Ok(majitel);
        }

        // UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMajitel(int id, [FromBody] MAJITELE majitel)
        {
            if (majitel == null || majitel.ID_MAJITEL != id)
                return BadRequest();

            var existingMajitel = await _majitelService.GetMajitelByIdAsync(id);
            if (existingMajitel == null)
                return NotFound();

            await _majitelService.UpdateMajitelAsync(majitel);
            return NoContent();
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMajitel(int id)
        {
            var majitel = await _majitelService.GetMajitelByIdAsync(id);
            if (majitel == null)
                return NotFound();

            await _majitelService.DeleteMajitelAsync(id);
            return NoContent();
        }
    }
}
