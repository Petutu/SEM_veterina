namespace Sem_Veterina.CRUD
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Sem_Veterina.Entity;

    public class MajitelService
    {
        private readonly OracleDbContext _context;

        public MajitelService(OracleDbContext context)
        {
            _context = context;
        }

        // CREATE
        public async Task AddMajitelAsync(MAJITELE majitel)
        {
            _context.MAJITELE.Add(majitel);
            await _context.SaveChangesAsync();
        }

        // READ - GET ALL
        public async Task<List<MAJITELE>> GetAllMajitelAsync()
        {
            return await _context.MAJITELE.ToListAsync();
        }

        // READ - GET BY ID
        public async Task<MAJITELE> GetMajitelByIdAsync(int id)
        {
            return await _context.MAJITELE.FindAsync(id);
        }

        // UPDATE
        public async Task UpdateMajitelAsync(MAJITELE majitel)
        {
            var existingMajitel = await _context.MAJITELE.FindAsync(majitel.ID_MAJITEL);
            if (existingMajitel != null)
            {
                existingMajitel.JMÉNO = majitel.JMÉNO;
                existingMajitel.PŘÍJMENÍ = majitel.PŘÍJMENÍ;
                existingMajitel.TELEFONNÍ_ČÍSLO = majitel.TELEFONNÍ_ČÍSLO;
                existingMajitel.ADRESA = majitel.ADRESA;
                existingMajitel.EMAIL = majitel.EMAIL;
                await _context.SaveChangesAsync();
            }
        }

        // DELETE
        public async Task DeleteMajitelAsync(int id)
        {
            var majitel = await _context.MAJITELE.FindAsync(id);
            if (majitel != null)
            {
                _context.MAJITELE.Remove(majitel);
                await _context.SaveChangesAsync();
            }
        }
    }
}
