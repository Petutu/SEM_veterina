using Microsoft.EntityFrameworkCore;
using Sem_Veterina.Entity;

namespace Sem_Veterina.CRUD
{
    public class PristrojeService
    {
        private readonly OracleDbContext _context;

        public PristrojeService(OracleDbContext context)
        {
            _context = context;
        }

        // CREATE
        public async Task AddPristrojAsync(PRISTROJE pristroj)
        {
            _context.Pristroje.Add(pristroj);
            await _context.SaveChangesAsync();
        }

        // READ - GET ALL
        public async Task<List<PRISTROJE>> GetAllPristrojeAsync()
        {
            return await _context.Pristroje.ToListAsync();
        }

        // READ - GET BY ID
        public async Task<PRISTROJE> GetPristrojByIdAsync(int id)
        {
            return await _context.Pristroje.FindAsync(id);
        }

        // UPDATE
        public async Task UpdatePristrojAsync(PRISTROJE pristroj)
        {
            var existingPristroj = await _context.Pristroje.FindAsync(pristroj.ID_PŘÍSTROJ);
            if (existingPristroj != null)
            {
                _context.Entry(existingPristroj).CurrentValues.SetValues(pristroj);
                await _context.SaveChangesAsync();
            }
        }

        // DELETE
        public async Task DeletePristrojAsync(int id)
        {
            var pristroj = await _context.Pristroje.FindAsync(id);
            if (pristroj != null)
            {
                _context.Pristroje.Remove(pristroj);
                await _context.SaveChangesAsync();
            }
        }
    }

}
