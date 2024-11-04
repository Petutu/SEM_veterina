using Microsoft.EntityFrameworkCore;
using Sem_Veterina.Entity;

namespace Sem_Veterina.CRUD
{
    public class DIAGNOZAService
    {
        private readonly OracleDbContext _context;

        public DIAGNOZAService(OracleDbContext context)
        {
            _context = context;
        }

        // CREATE
        public async Task AddDiagnozaAsync(DIAGNOZY diagnoza)
        {
            _context.Diagnozy.Add(diagnoza);
            await _context.SaveChangesAsync();
        }

        // READ - GET ALL
        public async Task<List<DIAGNOZY>> GetAllDiagnozyAsync()
        {
            return await _context.Diagnozy.ToListAsync();
        }

        // READ - GET BY ID
        public async Task<DIAGNOZY> GetDiagnozaByIdAsync(int id)
        {
            return await _context.Diagnozy.FindAsync(id);
        }

        // UPDATE
        public async Task UpdateDiagnozaAsync(DIAGNOZY diagnoza)
        {
            var existingDiagnoza = await _context.Diagnozy.FindAsync(diagnoza.ID_DIAGNÓZA);
            if (existingDiagnoza != null)
            {
                _context.Entry(existingDiagnoza).CurrentValues.SetValues(diagnoza);
                await _context.SaveChangesAsync();
            }
        }

        // DELETE
        public async Task DeleteDiagnozaAsync(int id)
        {
            var diagnoza = await _context.Diagnozy.FindAsync(id);
            if (diagnoza != null)
            {
                _context.Diagnozy.Remove(diagnoza);
                await _context.SaveChangesAsync();
            }
        }
    }

}
