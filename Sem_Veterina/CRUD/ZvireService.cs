using Microsoft.EntityFrameworkCore;
using Sem_Veterina.Entity;

namespace Sem_Veterina.CRUD
{
    public class ZvireService
    {
        private readonly OracleDbContext _context;

        public ZvireService(OracleDbContext context)
        {
            _context = context;
        }

        // CREATE
        public async Task AddZvireAsync(ZVIRATA zvire)
        {
            _context.Zvirata.Add(zvire);
            await _context.SaveChangesAsync();
        }

        // READ - GET ALL
        public async Task<List<ZVIRATA>> GetAllZvirataAsync()
        {
            return await _context.Zvirata.ToListAsync();
        }

        // READ - GET BY ID
        public async Task<ZVIRATA> GetZvireByIdAsync(int id)
        {
            return await _context.Zvirata.FindAsync(id);
        }

        // UPDATE
        public async Task UpdateZvireAsync(ZVIRATA zvire)
        {
            var existingZvire = await _context.Zvirata.FindAsync(zvire.ID_ZVÍŘE);
            if (existingZvire != null)
            {
                _context.Entry(existingZvire).CurrentValues.SetValues(zvire);
                await _context.SaveChangesAsync();
            }
        }

        // DELETE
        public async Task DeleteZvireAsync(int id)
        {
            var zvire = await _context.Zvirata.FindAsync(id);
            if (zvire != null)
            {
                _context.Zvirata.Remove(zvire);
                await _context.SaveChangesAsync();
            }
        }
    }
}
