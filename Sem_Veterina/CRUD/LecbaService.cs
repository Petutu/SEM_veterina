using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using Sem_Veterina;
using Sem_Veterina.Entity;

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
        var sql = "INSERT INTO LECBY (ID_LÉČBA, POPIS, ID_DIAGNÓZA) VALUES (:Id, :Popis, :IdDiagnoza)";
        await _context.Database.ExecuteSqlRawAsync(sql,
            new OracleParameter("Id", lecba.ID_LÉČBA),
            new OracleParameter("Popis", lecba.POPIS),
            new OracleParameter("IdDiagnoza", lecba.ID_DIAGNÓZA));
    }

    // READ - GET ALL
    public async Task<List<LECBY>> GetAllLecbyAsync()
    {
        var sql = "SELECT * FROM LECBY";
        return await _context.Lecby.FromSqlRaw(sql).ToListAsync();
    }

    // READ - GET BY ID
    public async Task<LECBY> GetLecbaByIdAsync(int id)
    {
        var sql = "SELECT * FROM LECBY WHERE ID_LÉČBA = :Id";
        var param = new OracleParameter("Id", id);
        return await _context.Lecby.FromSqlRaw(sql, param).FirstOrDefaultAsync();
    }

    // UPDATE
    public async Task UpdateLecbaAsync(LECBY lecba)
    {
        var sql = "UPDATE LECBY SET POPIS = :Popis, ID_DIAGNÓZA = :IdDiagnoza WHERE ID_LÉČBA = :Id";
        await _context.Database.ExecuteSqlRawAsync(sql,
            new OracleParameter("Popis", lecba.POPIS),
            new OracleParameter("IdDiagnoza", lecba.ID_DIAGNÓZA),
            new OracleParameter("Id", lecba.ID_LÉČBA));
    }

    // DELETE
    public async Task DeleteLecbaAsync(int id)
    {
        var sql = "DELETE FROM LECBY WHERE ID_LÉČBA = :Id";
        await _context.Database.ExecuteSqlRawAsync(sql, new OracleParameter("Id", id));
    }
}
