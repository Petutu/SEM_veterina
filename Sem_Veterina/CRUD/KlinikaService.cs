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
            return await _context.Kliniky.ToListAsync();
        }

        // READ - GET BY ID
        public async Task<KLINIKY> GetKlinikaByIdAsync(int id)
        {
            return await _context.Kliniky.FindAsync(id);
        }

        //READ - GER BY ADDRESS OR TELEFONNI_CISLO
        public async Task<List<KLINIKY>> GetFilteredKlinikyAsync(string? address, string? phone)
        {
            var query = _context.Kliniky.AsQueryable();

            if (!string.IsNullOrEmpty(address))
            {
                query = query.Where(k => k.ADRESA.Contains(address));
            }

            if (!string.IsNullOrEmpty(phone))
            {
                query = query.Where(k => k.TELEFONNÍ_ČÍSLO.ToString().Contains(phone));
            }

            return await query.ToListAsync();
        }

    }
}
