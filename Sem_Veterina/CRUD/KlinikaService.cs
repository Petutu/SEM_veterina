namespace Sem_Veterina.CRUD
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Sem_Veterina.Entity;

    public class KlinikaService
    {
        private readonly OracleDbContext _context;

        public KlinikaService(OracleDbContext context)
        {
            _context = context;
        }

       
        // READ - GET ALL
        public async Task<List<KLINIKY>> GetAllKlinikyAsync()
        {
            return await _context.KLINIKY.ToListAsync();
        }

        // READ - GET BY ID
        public async Task<KLINIKY> GetKlinikaByIdAsync(int id)
        {
            return await _context.KLINIKY.FindAsync(id);
        }

      
    }
}
