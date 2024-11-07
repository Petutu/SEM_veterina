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
            _context.Majitele.Add(majitel);
            await _context.SaveChangesAsync();
        }

        // READ - GET ALL
        public async Task<List<MAJITELE>> GetAllMajiteleAsync()
        {
            return await _context.Majitele.ToListAsync();
        }

        // READ - GET BY ID
        public async Task<MAJITELE> GetMajitelByIdAsync(int id)
        {
            return await _context.Majitele.FindAsync(id);
        }

        //READ - GER BY ADDRESS OR TELEFONNI_CISLO
        public async Task<List<MAJITELE>> GetFilteredMajiteleAsync(string? name, string? lastname, string? phone)
        {
            var query = _context.Majitele.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(m => m.JMÉNO.Contains(name));
            }

            if (!string.IsNullOrEmpty(phone))
            {
                query = query.Where(m => m.PŘÍJMENÍ.Contains(lastname));
            }

            if (!string.IsNullOrEmpty(phone))
            {
                query = query.Where(m => m.TELEFONNÍ_ČÍSLO.ToString().Contains(phone));
            }

            return await query.ToListAsync();
        }

        // UPDATE
        public async Task UpdateMajitelAsync(MAJITELE majitel)
        {
            var existingMajitel = await _context.Majitele.FindAsync(majitel.ID_MAJITEL);
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
            var majitel = await _context.Majitele.FindAsync(id);
            if (majitel != null)
            {
                _context.Majitele.Remove(majitel);
                await _context.SaveChangesAsync();
            }
        }
    }
}
