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
            var sql = "BEGIN PROC_CREATE_ZDRAVOTNI_AKCI(:Datum, :IdPersonal, :IdKlinika, :PopisAkce, :IdPristroj, :IdZvire, :IdMajitel, :Vysledek); END;";
            await _context.Database.ExecuteSqlRawAsync(sql,
                new OracleParameter("Datum", akce.DATUM),
                new OracleParameter("IdPersonal", akce.ID_PRESONÁL),
                new OracleParameter("IdKlinika", akce.ID_KLINIKA),
                new OracleParameter("PopisAkce", akce.POPIS_AKCE),
                new OracleParameter("IdPristroj", akce.ID_PŘÍSTROJ),
                new OracleParameter("IdZvire", akce.ID_ZVÍŘE),
                new OracleParameter("IdMajitel", akce.ID_MAJITEL),
                new OracleParameter("Vysledek", akce.VÝSLEDEK));
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
            
            
                var sql = "BEGIN PROC_EDIT_ZDRAVOTNI_AKCI(:IdAkce, :Datum, :IdPersonal, :IdKlinika, :PopisAkce, :IdPristroj, :IdZvire, :IdMajitel, :Vysledek); END;";
                await _context.Database.ExecuteSqlRawAsync(sql,
                    new OracleParameter("IdAkce", akce.ID_AKCE),
                    new OracleParameter("Datum", akce.DATUM),
                    new OracleParameter("IdPersonal", akce.ID_PRESONÁL),
                    new OracleParameter("IdKlinika", akce.ID_KLINIKA),
                    new OracleParameter("PopisAkce", akce.POPIS_AKCE),
                    new OracleParameter("IdPristroj", akce.ID_PŘÍSTROJ),
                    new OracleParameter("IdZvire", akce.ID_ZVÍŘE),
                    new OracleParameter("IdMajitel", akce.ID_MAJITEL),
                    new OracleParameter("Vysledek", akce.VÝSLEDEK));
            

        }

        // DELETE
        public async Task DeleteZdravotniAkceAsync(int idAkce)
        {
            var sql = "BEGIN PROC_DELETE_ZDRAVOTNI_AKCI(:IdAkce); END;";
            await _context.Database.ExecuteSqlRawAsync(sql,
                new OracleParameter("IdAkce", idAkce));
        }

    }
}
