using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Sem_Veterina.Controllers
{
    public class ConnectionTestController : Controller
    {
        // GET: ConnectionTestController
        private readonly DbTestService _dbTestService;

        public ConnectionTestController(DbTestService dbTestService)
        {
            _dbTestService = dbTestService;
        }

        [HttpGet]
        [Route("test-connection")]
        public IActionResult TestConnection()
        {
            bool isConnected = _dbTestService.TestConnection();
            if (isConnected)
                return Ok("Připojení k databázi bylo úspěšné!");
            else
                return StatusCode(500, "Připojení k databázi se nezdařilo.");
        }
    }

    public class DbTestService
    {
        private readonly OracleDbContext _context;

        public DbTestService(OracleDbContext context)
        {
            _context = context;
        }

        public bool TestConnection()
        {
            try
            {
                _context.Database.OpenConnection();
                _context.Database.CloseConnection();
                Console.WriteLine("Připojení k databázi bylo úspěšné.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Chyba připojení: {ex.Message}");
                return false;
            }
        }
    }
}
