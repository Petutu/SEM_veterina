using Sem_Veterina.Entity;
using Sem_Veterina;
using Microsoft.EntityFrameworkCore;

public class LekyService
{
    private readonly OracleDbContext _context;

    public LekyService(OracleDbContext context)
    {
        _context = context;
    }

    // CREATE
    public async Task AddLekAsync(LEKY lek)
    {
        _context.Leky.Add(lek);
        await _context.SaveChangesAsync();
    }

    // READ - GET ALL
    public async Task<List<LEKY>> GetAllLekyAsync()
    {
        return await _context.Leky.ToListAsync();
    }

    // READ - GET BY ID
    public async Task<LEKY> GetLekByIdAsync(int id)
    {
        return await _context.Leky.FindAsync(id);
    }

    // UPDATE
    public async Task UpdateLekAsync(LEKY lek)
    {
        var existingLek = await _context.Leky.FindAsync(lek.ID_LÉK);
        if (existingLek != null)
        {
            _context.Entry(existingLek).CurrentValues.SetValues(lek);
            await _context.SaveChangesAsync();
        }
    }

    // DELETE
    public async Task DeleteLekAsync(int id)
    {
        var lek = await _context.Leky.FindAsync(id);
        if (lek != null)
        {
            _context.Leky.Remove(lek);
            await _context.SaveChangesAsync();
        }
    }
}
