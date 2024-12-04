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
        var sql = "BEGIN PROC_CREATE_LECBA(:Popis, :IdDiagnoza); END;";
        await _context.Database.ExecuteSqlRawAsync(sql,
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
        var sql = "BEGIN PROC_EDIT_LECBA(:IdLecba, :Popis, :IdDiagnoza); END;";
        await _context.Database.ExecuteSqlRawAsync(sql,
            new OracleParameter("IdLecba", lecba.ID_LÉČBA),
            new OracleParameter("Popis", lecba.POPIS),
            new OracleParameter("IdDiagnoza", lecba.ID_DIAGNÓZA));
    }


    // DELETE
    public async Task DeleteLecbaAsync(int idLecba)
    {
        var sql = "BEGIN PROC_DELETE_LECBA(:IdLecba); END;";
        await _context.Database.ExecuteSqlRawAsync(sql,
            new OracleParameter("IdLecba", idLecba));
    }


    public async Task<List<LECBY>> GetFilteredLecbyAsync(string? description)
    {
        var sql = "SELECT * FROM LECBY WHERE (:Description IS NULL OR POPIS LIKE '%' || :Description || '%')";

        var parameters = new[]
        {
        new OracleParameter("Description", description ?? (object)DBNull.Value)
    };

        return await _context.Lecby.FromSqlRaw(sql, parameters).ToListAsync();
    }

}
