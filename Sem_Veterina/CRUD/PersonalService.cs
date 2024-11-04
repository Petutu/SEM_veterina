using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sem_Veterina.Entity;

namespace Sem_Veterina.CRUD
{
    public class PersonalService
    {
        private readonly OracleDbContext _context;

        public PersonalService(OracleDbContext context)
        {
            _context = context;
        }

        // CREATE
        public async Task AddPersonalAsync(PERSONAL personal)
        {
            _context.Personal.Add(personal);
            await _context.SaveChangesAsync();
        }

        // READ - GET ALL
        public async Task<List<PERSONAL>> GetAllPersonalAsync()
        {
            return await _context.Personal.ToListAsync();
        }

        // READ - GET BY ID
        public async Task<PERSONAL> GetPersonalByIdAsync(int id)
        {
            return await _context.Personal.FindAsync(id);
        }

        // UPDATE
        public async Task UpdatePersonalAsync(PERSONAL personal)
        {
            var existingPersonal = await _context.Personal.FindAsync(personal.ID_PRESONÁL);
            if (existingPersonal != null)
            {
                _context.Entry(existingPersonal).CurrentValues.SetValues(personal);
                await _context.SaveChangesAsync();
            }
        }

        // DELETE
        public async Task DeletePersonalAsync(int id)
        {
            var personal = await _context.Personal.FindAsync(id);
            if (personal != null)
            {
                _context.Personal.Remove(personal);
                await _context.SaveChangesAsync();
            }
        }
    }
}

