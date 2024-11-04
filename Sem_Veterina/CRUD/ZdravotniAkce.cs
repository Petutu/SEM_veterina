using Microsoft.EntityFrameworkCore;
using Sem_Veterina.Entity;

namespace Sem_Veterina.CRUD
{
    public class ZdravotniAkceService
    {
        private readonly OracleDbContext _context;

        public ZdravotniAkceService(OracleDbContext context)
        {
            _context = context;
        }

        // CREATE
        public async Task AddZdravotniAkceAsync(ZDRAVOTNIAKCE akce)
        {
            _context.ZdravotniAkce.Add(akce);
            await _context.SaveChangesAsync();
        }

        // READ - GET ALL
        public async Task<List<ZDRAVOTNIAKCE>> GetAllZdravotniAkceAsync()
        {
            return await _context.ZdravotniAkce.ToListAsync();
        }

        // READ - GET BY ID
        public async Task<ZDRAVOTNIAKCE> GetZdravotniAkceByIdAsync(int id)
        {
            return await _context.ZdravotniAkce.FindAsync(id);
        }

        // UPDATE
        public async Task UpdateZdravotniAkceAsync(ZDRAVOTNIAKCE akce)
        {
            var existingAkce = await _context.ZdravotniAkce.FindAsync(akce.ID_AKCE);
            if (existingAkce != null)
            {
                _context.Entry(existingAkce).CurrentValues.SetValues(akce);
                await _context.SaveChangesAsync();
            }
        }

        // DELETE
        public async Task DeleteZdravotniAkceAsync(int id)
        {
            var akce = await _context.ZdravotniAkce.FindAsync(id);
            if (akce != null)
            {
                _context.ZdravotniAkce.Remove(akce);
                await _context.SaveChangesAsync();
            }
        }
    }

}
