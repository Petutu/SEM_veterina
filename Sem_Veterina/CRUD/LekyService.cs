using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using Sem_Veterina;
using Sem_Veterina.Entity;

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
        var sql = "BEGIN PROC_CREATE_LEK(:Nazev, :Davkovani, :Pokyny, :Ucinky, :IdLecba, :IdDiagnoza); END;";
        await _context.Database.ExecuteSqlRawAsync(sql,
            new OracleParameter("Nazev", lek.NÁZEV),
            new OracleParameter("Davkovani", lek.DÁVKOVÁNÍ),
            new OracleParameter("Pokyny", lek.POKYNY),
            new OracleParameter("Ucinky", lek.ÚČINKY ?? (object)DBNull.Value),
            new OracleParameter("IdLecba", lek.ID_LÉČBA),
            new OracleParameter("IdDiagnoza", lek.ID_DIAGNÓZA));
    }


    // READ - GET ALL
    public async Task<List<LEKY>> GetAllLekyAsync()
    {
        var sql = "SELECT * FROM LEKY";
        return await _context.Leky.FromSqlRaw(sql).ToListAsync();
    }

    // READ - GET BY ID
    public async Task<LEKY> GetLekByIdAsync(int id)
    {
        var sql = "SELECT * FROM LEKY WHERE ID_LÉK = :Id";
        var param = new OracleParameter("Id", id);
        return await _context.Leky.FromSqlRaw(sql, param).FirstOrDefaultAsync();
    }

    // UPDATE
    public async Task UpdateLekAsync(LEKY lek)
    {
        var sql = "UPDATE LEKY SET NÁZEV = :Nazev, DÁVKOVÁNÍ = :Davkovani, POKYNY = :Pokyny, " +
                  "ÚČINKY = :Ucinky, ID_LÉČBA = :IdLecba WHERE ID_LÉK = :Id";
        await _context.Database.ExecuteSqlRawAsync(sql,
            new OracleParameter("Nazev", lek.NÁZEV),
            new OracleParameter("Davkovani", lek.DÁVKOVÁNÍ),
            new OracleParameter("Pokyny", lek.POKYNY),
            new OracleParameter("Ucinky", lek.ÚČINKY ?? (object)DBNull.Value),
            new OracleParameter("IdLecba", lek.ID_LÉČBA),
            new OracleParameter("Id", lek.ID_LÉK));
    }

    // DELETE
    public async Task DeleteLekAsync(int id)
    {
        var sql = "DELETE FROM LEKY WHERE ID_LÉK = :Id";
        await _context.Database.ExecuteSqlRawAsync(sql, new OracleParameter("Id", id));
    }

    public async Task<List<LEKY>> GetFilteredLekyAsync(string? name, string? instructions)
    {
        var sql = @"SELECT * FROM LEKY WHERE (:Name IS NULL OR NÁZEV LIKE '%' || :Name || '%') 
                    AND (:Instructions IS NULL OR INSTRUKCE LIKE '%' || :Instructions || '%')";

        var parameters = new[]
        {
            new OracleParameter("Name", name ?? (object)DBNull.Value),
            new OracleParameter("Instructions", instructions ?? (object)DBNull.Value),
        };

        return await _context.Leky.FromSqlRaw(sql, parameters).ToListAsync();
    }

}
