using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using Sem_Veterina.Entity;

namespace Sem_Veterina.CRUD
{
    public class KlinikaService
    {
        private readonly OracleDbContext _context;

        public KlinikaService(OracleDbContext context)
        {
            _context = context;
        }

        // READ - GET ALL
        public async Task<List<KLINIKY>> GetAllKlinikyAsync()
        {
            var sql = "SELECT * FROM KLINIKY";
            return await _context.Kliniky.FromSqlRaw(sql).ToListAsync();
        }

        // READ - GET BY ID
        public async Task<KLINIKY> GetKlinikaByIdAsync(int id)
        {
            var sql = "SELECT * FROM KLINIKY WHERE ID_KLINIKA = :Id";
            var param = new OracleParameter("Id", id);
            return await _context.Kliniky.FromSqlRaw(sql, param).FirstOrDefaultAsync();
        }

        // READ - GET BY ADDRESS OR TELEFONNI_CISLO
        public async Task<List<KLINIKY>> GetFilteredKlinikyAsync(string? address, string? phone)
        {
            var sql = "SELECT * FROM KLINIKY WHERE (:Address IS NULL OR ADRESA LIKE '%' || :Address || '%') " +
                      "AND (:Phone IS NULL OR TELEFONNÍ_ČÍSLO LIKE '%' || :Phone || '%')";

            var parameters = new[]
            {
                new OracleParameter("Address", address ?? (object)DBNull.Value),
                new OracleParameter("Phone", phone ?? (object)DBNull.Value)
            };

            return await _context.Kliniky.FromSqlRaw(sql, parameters).ToListAsync();
        }
    }
}