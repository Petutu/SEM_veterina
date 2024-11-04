using Microsoft.EntityFrameworkCore;
using Sem_Veterina.Entity;

namespace Sem_Veterina.CRUD
{
    public class LecbaService
    {
        private readonly OracleDbContext _context;

        public LecbaService(OracleDbContext context)
        {
            _context = context;
        }

        // CREATE
        public async Task AddLecbaAsync(LECBY lecba)
        {
            _context.Lecby.Add(lecba);
            await _context.SaveChangesAsync();
        }

        // READ - GET ALL
        public async Task<List<LECBY>> GetAllLecbyAsync()
        {
            return await _context.Lecby.ToListAsync();
        }

        // READ - GET BY ID
        public async Task<LECBY> GetLecbaByIdAsync(int id)
        {
            return await _context.Lecby.FindAsync(id);
        }

        // UPDATE
        public async Task UpdateLecbaAsync(LECBY lecba)
        {
            var existingLecba = await _context.Lecby.FindAsync(lecba.ID_LÉČBA);
            if (existingLecba != null)
            {
                _context.Entry(existingLecba).CurrentValues.SetValues(lecba);
                await _context.SaveChangesAsync();
            }
        }

        // DELETE
        public async Task DeleteLecbaAsync(int id)
        {
            var lecba = await _context.Lecby.FindAsync(id);
            if (lecba != null)
            {
                _context.Lecby.Remove(lecba);
                await _context.SaveChangesAsync();
            }
        }
    }

}
