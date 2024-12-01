using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using Sem_Veterina.Entity;

namespace Sem_Veterina.CRUD
{
    public class ZvireService
    {
        private readonly OracleDbContext _context;

        public ZvireService(OracleDbContext context)
        {
            _context = context;
        }

        // CREATE
        public async Task AddZvireAsync(ZVIRATA zvire)
        {
            var sql = "INSERT INTO ZVIRATA (ID_ZVÍŘE, JMÉNO, DRUH, VĚK, ZDRAVOTNÍ_STAV, VÁHA, ID_MAJITEL) " +
                      "VALUES (:Id, :Jmeno, :Druh, :Vek, :ZdravotniStav, :Vaha, :IdMajitel)";
            await _context.Database.ExecuteSqlRawAsync(sql,
                new OracleParameter("Id", zvire.ID_ZVÍŘE),
                new OracleParameter("Jmeno", zvire.JMÉNO),
                new OracleParameter("Druh", zvire.DRUH),
                new OracleParameter("Vek", zvire.VĚK),
                new OracleParameter("ZdravotniStav", zvire.ZDRAVOTNÍ_STAV),
                new OracleParameter("Vaha", zvire.VÁHA),
                new OracleParameter("IdMajitel", zvire.ID_MAJITEL));
        }

        // READ - GET ALL
        public async Task<List<ZVIRATA>> GetAllZvirataAsync()
        {
            var sql = "SELECT * FROM ZVIRATA";
            return await _context.Zvirata.FromSqlRaw(sql).ToListAsync();
        }

        // READ - GET BY ID
        public async Task<ZVIRATA> GetZvireByIdAsync(int id)
        {
            var sql = "SELECT * FROM ZVIRATA WHERE ID_ZVÍŘE = :Id";
            var param = new OracleParameter("Id", id);
            return await _context.Zvirata.FromSqlRaw(sql, param).FirstOrDefaultAsync();
        }

        // UPDATE
        public async Task UpdateZvireAsync(ZVIRATA zvire)
        {
            var sql = "UPDATE ZVIRATA SET JMÉNO = :Jmeno, DRUH = :Druh, VĚK = :Vek, ZDRAVOTNÍ_STAV = :ZdravotniStav, " +
                      "VÁHA = :Vaha, ID_MAJITEL = :IdMajitel WHERE ID_ZVÍŘE = :Id";
            await _context.Database.ExecuteSqlRawAsync(sql,
                new OracleParameter("Jmeno", zvire.JMÉNO),
                new OracleParameter("Druh", zvire.DRUH),
                new OracleParameter("Vek", zvire.VĚK),
                new OracleParameter("ZdravotniStav", zvire.ZDRAVOTNÍ_STAV),
                new OracleParameter("Vaha", zvire.VÁHA),
                new OracleParameter("IdMajitel", zvire.ID_MAJITEL),
                new OracleParameter("Id", zvire.ID_ZVÍŘE));
        }

        // DELETE
        public async Task DeleteZvireAsync(int id)
        {
            var sql = "DELETE FROM ZVIRATA WHERE ID_ZVÍŘE = :Id";
            await _context.Database.ExecuteSqlRawAsync(sql, new OracleParameter("Id", id));
        }

        public async Task<List<ZVIRATA>> GetFilteredZvirataAsync(string? name, string? species)
        {
            var sql = "SELECT * FROM ZVIRATA WHERE (:Name IS NULL OR JMÉNO LIKE '%' || :Name || '%') " +
                      "AND (:Species IS NULL OR DRUH LIKE '%' || :Species || '%')";

            var parameters = new[]
            {
                new OracleParameter("Name", name ?? (object)DBNull.Value),
                new OracleParameter("Species", species ?? (object)DBNull.Value)
            };

            return await _context.Zvirata.FromSqlRaw(sql, parameters).ToListAsync();
        }
    }
}
