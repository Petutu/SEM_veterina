using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using Sem_Veterina.Entity;

namespace Sem_Veterina.CRUD
{
    public class PristrojeService
    {
        private readonly OracleDbContext _context;

        public PristrojeService(OracleDbContext context)
        {
            _context = context;
        }

        // CREATE
        public async Task AddPristrojAsync(PRISTROJE pristroj)
        {
            var sql = "INSERT INTO PRISTROJE (ID_PŘÍSTROJ, NÁZEV, FUNKCE, ID_KLINIKA) " +
                      "VALUES (:Id, :Nazev, :Funkce, :IdKlinika)";
            await _context.Database.ExecuteSqlRawAsync(sql,
                new OracleParameter("Id", pristroj.ID_PŘÍSTROJ),
                new OracleParameter("Nazev", pristroj.NÁZEV),
                new OracleParameter("Funkce", pristroj.FUNKCE),
                new OracleParameter("IdKlinika", pristroj.ID_KLINIKA));
        }

        // READ - GET ALL
        public async Task<List<PRISTROJE>> GetAllPristrojeAsync()
        {
            var sql = "SELECT * FROM PRISTROJE";
            return await _context.Pristroje.FromSqlRaw(sql).ToListAsync();
        }

        // READ - GET BY ID
        public async Task<PRISTROJE> GetPristrojByIdAsync(int id)
        {
            var sql = "SELECT * FROM PRISTROJE WHERE ID_PŘÍSTROJ = :Id";
            var param = new OracleParameter("Id", id);
            return await _context.Pristroje.FromSqlRaw(sql, param).FirstOrDefaultAsync();
        }

        // UPDATE
        public async Task UpdatePristrojAsync(PRISTROJE pristroj)
        {
            var sql = "UPDATE PRISTROJE SET NÁZEV = :Nazev, FUNKCE = :Funkce, ID_KLINIKA = :IdKlinika " +
                      "WHERE ID_PŘÍSTROJ = :Id";
            await _context.Database.ExecuteSqlRawAsync(sql,
                new OracleParameter("Nazev", pristroj.NÁZEV),
                new OracleParameter("Funkce", pristroj.FUNKCE),
                new OracleParameter("IdKlinika", pristroj.ID_KLINIKA),
                new OracleParameter("Id", pristroj.ID_PŘÍSTROJ));
        }

        // DELETE
        public async Task DeletePristrojAsync(int id)
        {
            var sql = "DELETE FROM PRISTROJE WHERE ID_PŘÍSTROJ = :Id";
            await _context.Database.ExecuteSqlRawAsync(sql, new OracleParameter("Id", id));
        }
        public async Task<List<PRISTROJE>> GetFilteredPristrojeAsync(string? name, string? function, int? klinikaId)
        {
            var sql = "SELECT * FROM PRISTROJE WHERE (:Name IS NULL OR NÁZEV LIKE '%' || :Name || '%') " +
                      "AND (:Function IS NULL OR FUNKCE LIKE '%' || :Function || '%') " +
                      "AND (:KlinikaId IS NULL OR ID_KLINIKA = :KlinikaId)";

            var parameters = new[]
            {
        new OracleParameter("Name", name ?? (object)DBNull.Value),
        new OracleParameter("Function", function ?? (object)DBNull.Value),
        new OracleParameter("KlinikaId", klinikaId ?? (object)DBNull.Value)
    };

            return await _context.Pristroje.FromSqlRaw(sql, parameters).ToListAsync();
        }



    }

}
