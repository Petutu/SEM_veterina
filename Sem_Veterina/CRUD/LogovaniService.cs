using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using Sem_Veterina.Entity;

namespace Sem_Veterina.CRUD
{
    public class LogovaniService
    {
        private readonly OracleDbContext _context;

        public LogovaniService(OracleDbContext context)
        {
            _context = context;
        }

        // CREATE
        public async Task AddLogAsync(LOGOVANI log)
        {
            var sql = "INSERT INTO LOGOVANI (ID_LOGOVANI, NAZEV_TABULKY, ID_ZAZNAMU, OPERACE, CAS_PROVEDENI, POPIS_ZMENY) " +
                      "VALUES (:IdLogovani, :NazevTabulky, :IdZaznamu, :Operace, :CasProvedeni, :PopisZmeny)";
            await _context.Database.ExecuteSqlRawAsync(sql,
                new OracleParameter("IdLogovani", log.ID_LOGOVANI),
                new OracleParameter("NazevTabulky", log.NAZEV_TABULKY),
                new OracleParameter("IdZaznamu", log.ID_ZAZNAMU),
                new OracleParameter("Operace", log.OPERACE),
                new OracleParameter("CasProvedeni", log.CAS_PROVEDENI),
                new OracleParameter("PopisZmeny", log.POPIS_ZMENY));
        }

        // READ - GET ALL
        public async Task<List<LOGOVANI>> GetAllLogsAsync()
        {
            var sql = "SELECT * FROM LOGOVANI";
            return await _context.Logovani.FromSqlRaw(sql).ToListAsync();
        }

        // READ - GET BY ID
        public async Task<LOGOVANI> GetLogByIdAsync(int id)
        {
            var sql = "SELECT * FROM LOGOVANI WHERE ID_LOGOVANI = :IdLogovani";
            var param = new OracleParameter("IdLogovani", id);
            return await _context.Logovani.FromSqlRaw(sql, param).FirstOrDefaultAsync();
        }

        // UPDATE
        public async Task UpdateLogAsync(LOGOVANI log)
        {
            var sql = "UPDATE LOGOVANI SET NAZEV_TABULKY = :NazevTabulky, ID_ZAZNAMU = :IdZaznamu, " +
                      "OPERACE = :Operace, CAS_PROVEDENI = :CasProvedeni, POPIS_ZMENY = :PopisZmeny " +
                      "WHERE ID_LOGOVANI = :IdLogovani";
            await _context.Database.ExecuteSqlRawAsync(sql,
                new OracleParameter("NazevTabulky", log.NAZEV_TABULKY),
                new OracleParameter("IdZaznamu", log.ID_ZAZNAMU),
                new OracleParameter("Operace", log.OPERACE),
                new OracleParameter("CasProvedeni", log.CAS_PROVEDENI),
                new OracleParameter("PopisZmeny", log.POPIS_ZMENY),
                new OracleParameter("IdLogovani", log.ID_LOGOVANI));
        }

        // DELETE
        public async Task DeleteLogAsync(int id)
        {
            var sql = "DELETE FROM LOGOVANI WHERE ID_LOGOVANI = :IdLogovani";
            await _context.Database.ExecuteSqlRawAsync(sql, new OracleParameter("IdLogovani", id));
        }

        public async Task<List<LOGOVANI>> GetFilteredLogovaniAsync(string? tableName, string? operation)
        {
            var sql = "SELECT * FROM LOGOVANI WHERE (:TableName IS NULL OR NAZEV_TABULKY LIKE '%' || :TableName || '%') " +
                      "AND (:Operation IS NULL OR OPERACE LIKE '%' || :Operation || '%')";

            var parameters = new[]
            {
        new OracleParameter("TableName", tableName ?? (object)DBNull.Value),
        new OracleParameter("Operation", operation ?? (object)DBNull.Value)
    };

            return await _context.Logovani.FromSqlRaw(sql, parameters).ToListAsync();
        }

    }
}