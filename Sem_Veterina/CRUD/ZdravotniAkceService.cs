using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
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
            var sql = "INSERT INTO ZDRAVOTNI_AKCI (ID_AKCE, DATUM, POPIS_AKCE, VÝSLEDEK, ID_PRESONÁL, ID_PŘÍSTROJ, ID_ZVÍŘE) " +
                      "VALUES (:Id, :Datum, :PopisAkce, :Vysledek, :IdPersonal, :IdPristroj, :IdZvire)";
            await _context.Database.ExecuteSqlRawAsync(sql,
                new OracleParameter("Id", akce.ID_AKCE),
                new OracleParameter("Datum", akce.DATUM),
                new OracleParameter("PopisAkce", akce.POPIS_AKCE),
                new OracleParameter("Vysledek", akce.VÝSLEDEK),
                new OracleParameter("IdPersonal", akce.ID_PRESONÁL),
                new OracleParameter("IdPristroj", akce.ID_PŘÍSTROJ),
                new OracleParameter("IdZvire", akce.ID_ZVÍŘE));
        }

        // READ - GET ALL
        public async Task<List<ZDRAVOTNIAKCE>> GetAllZdravotniAkceAsync()
        {
            var sql = "SELECT * FROM ZDRAVOTNI_AKCI";
            return await _context.ZdravotniAkce.FromSqlRaw(sql).ToListAsync();
        }

        // READ - GET BY ID
        public async Task<ZDRAVOTNIAKCE> GetZdravotniAkceByIdAsync(int id)
        {
            var sql = "SELECT * FROM ZDRAVOTNI_AKCI WHERE ID_AKCE = :Id";
            var param = new OracleParameter("Id", id);
            return await _context.ZdravotniAkce.FromSqlRaw(sql, param).FirstOrDefaultAsync();
        }

        // UPDATE
        public async Task UpdateZdravotniAkceAsync(ZDRAVOTNIAKCE akce)
        {
            var sql = "UPDATE ZDRAVOTNI_AKCI SET DATUM = :Datum, POPIS_AKCE = :PopisAkce, VÝSLEDEK = :Vysledek, " +
                      "ID_PRESONÁL = :IdPersonal, ID_PŘÍSTROJ = :IdPristroj, ID_ZVÍŘE = :IdZvire WHERE ID_AKCE = :Id";
            await _context.Database.ExecuteSqlRawAsync(sql,
                new OracleParameter("Datum", akce.DATUM),
                new OracleParameter("PopisAkce", akce.POPIS_AKCE),
                new OracleParameter("Vysledek", akce.VÝSLEDEK),
                new OracleParameter("IdPersonal", akce.ID_PRESONÁL),
                new OracleParameter("IdPristroj", akce.ID_PŘÍSTROJ),
                new OracleParameter("IdZvire", akce.ID_ZVÍŘE),
                new OracleParameter("Id", akce.ID_AKCE));
        }

        // DELETE
        public async Task DeleteZdravotniAkceAsync(int id)
        {
            var sql = "DELETE FROM ZDRAVOTNI_AKCI WHERE ID_AKCE = :Id";
            await _context.Database.ExecuteSqlRawAsync(sql, new OracleParameter("Id", id));
        }
    }
}
