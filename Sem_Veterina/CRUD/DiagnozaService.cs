using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using Sem_Veterina;
using Sem_Veterina.Entity;

public class DiagnozaService
{
    private readonly OracleDbContext _context;

    public DiagnozaService(OracleDbContext context)
    {
        _context = context;
    }

    // CREATE
    public async Task AddDiagnozaAsync(DIAGNOZY diagnoza)
    {
        var sql = "INSERT INTO DIAGNOZY (ID_DIAGNÓZA, NÁZEV, POPIS, ID_ZVÍŘE, Id_PRESONÁL) " +
                  "VALUES (:Id, :Nazev, :Popis, :IdZvire, :IdPersonal)";
        await _context.Database.ExecuteSqlRawAsync(sql,
            new OracleParameter("Id", diagnoza.ID_DIAGNÓZA),
            new OracleParameter("Nazev", diagnoza.NÁZEV),
            new OracleParameter("Popis", diagnoza.POPIS),
            new OracleParameter("IdZvire", diagnoza.ID_ZVÍŘE),
            new OracleParameter("IdPersonal", diagnoza.ID_PERSONÁL));
    }

    // READ - GET ALL
    public async Task<List<DIAGNOZY>> GetAllDiagnozyAsync()
    {
        var sql = "SELECT * FROM DIAGNOZY";
        return await _context.Diagnozy.FromSqlRaw(sql).ToListAsync();
    }

    // READ - GET BY ID
    public async Task<DIAGNOZY> GetDiagnozaByIdAsync(int id)
    {
        var sql = "SELECT * FROM DIAGNOZY WHERE ID_DIAGNÓZA = :Id";
        var param = new OracleParameter("Id", id);
        return await _context.Diagnozy.FromSqlRaw(sql, param).FirstOrDefaultAsync();
    }

    // UPDATE
    public async Task UpdateDiagnozaAsync(DIAGNOZY diagnoza)
    {
        var sql = "UPDATE DIAGNOZY SET NÁZEV = :Nazev, POPIS = :Popis, ID_ZVÍŘE = :IdZvire, Id_PRESONÁL = :IdPersonal " +
                  "WHERE ID_DIAGNÓZA = :Id";
        await _context.Database.ExecuteSqlRawAsync(sql,
            new OracleParameter("Nazev", diagnoza.NÁZEV),
            new OracleParameter("Popis", diagnoza.POPIS),
            new OracleParameter("IdZvire", diagnoza.ID_ZVÍŘE),
            new OracleParameter("IdPersonal", diagnoza.ID_PERSONÁL),
            new OracleParameter("Id", diagnoza.ID_DIAGNÓZA));
    }

    // DELETE
    public async Task DeleteDiagnozaAsync(int id)
    {
        var sql = "DELETE FROM DIAGNOZY WHERE ID_DIAGNÓZA = :Id";
        await _context.Database.ExecuteSqlRawAsync(sql, new OracleParameter("Id", id));
    }
}
