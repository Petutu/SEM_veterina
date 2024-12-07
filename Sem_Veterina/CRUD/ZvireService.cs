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

        // READ - GET ALL
        public async Task<List<ZVIRATA>> GetAllZvirataAsync()
        {
            var sql = "SELECT * FROM ZVIRATA";
            return await _context.Zvirata.FromSqlRaw(sql).ToListAsync();
        }

        // READ - GET BY ID
        public async Task<ZvireDetailView> GetZvireByIdAsync(int id)
        {
            var sql = "SELECT * FROM V_ZVIRE_DETAILS WHERE ID_ZVÍŘE = :Id";
            var param = new OracleParameter("Id", id);
            return await _context.ZvireDetailView.FromSqlRaw(sql, param).FirstOrDefaultAsync();
        }

        public async Task<List<ZvireDetailView>> GetFilteredZvirataAsync(string? name, string? species)
        {
            var sql = "SELECT * FROM V_ZVIRE_DETAILS WHERE (:Name IS NULL OR JMÉNO_ZVÍŘETE LIKE '%' || :Name || '%') " +
                      "AND (:Species IS NULL OR DRUH LIKE '%' || :Species || '%')";

            var parameters = new[]
            {
                new OracleParameter("Name", name ?? (object)DBNull.Value),
                new OracleParameter("Species", species ?? (object)DBNull.Value)
            };

            return await _context.ZvireDetailView.FromSqlRaw(sql, parameters).ToListAsync();
        }

        // CREATE
        public async Task AddZvireAsync(ZVIRATA zvire)
        {
            var sql = "BEGIN CREATE_ZVIRE(:JMENO, :DRUH, :VEK, :ZDRAVOTNI_STAV, :ID_MAJITEL, :VAHA); END;";
            await _context.Database.ExecuteSqlRawAsync(sql,
                new OracleParameter("JMENO", zvire.JMÉNO),
                new OracleParameter("DRUH", zvire.DRUH),
                new OracleParameter("VEK", zvire.VĚK),
                new OracleParameter("ZDRAVOTNI_STAV", zvire.ZDRAVOTNÍ_STAV),
                new OracleParameter("ID_MAJITEL", zvire.ID_MAJITEL),
                new OracleParameter("VAHA", zvire.VÁHA));
        }

        // UPDATE
        public async Task UpdateZvireAsync(ZVIRATA zvire)
        {

            var sql = "BEGIN EDIT_ZVIRE(:ID_ZVIRE, :JMENO, :DRUH, :VEK, :ZDRAVOTNI_STAV, :ID_MAJITEL, :VAHA); END;";
            await _context.Database.ExecuteSqlRawAsync(sql,
                new OracleParameter("ID_ZVIRE", zvire.ID_ZVÍŘE),
                new OracleParameter("JMENO", zvire.JMÉNO),
                new OracleParameter("DRUH", zvire.DRUH),
                new OracleParameter("VEK", zvire.VĚK),
                new OracleParameter("ZDRAVOTNI_STAV", zvire.ZDRAVOTNÍ_STAV),
                new OracleParameter("ID_MAJITEL", zvire.ID_MAJITEL),
                new OracleParameter("VAHA", zvire.VÁHA));

        }

        // DELETE
        public async Task DeleteZvireAsync(int id)
        {
            var sql = "BEGIN DELETE_ZVIRE(:ID_ZVIRE); END;";
            await _context.Database.ExecuteSqlRawAsync(sql,
                new OracleParameter("ID_ZVIRE", id));
        }

    }
}
