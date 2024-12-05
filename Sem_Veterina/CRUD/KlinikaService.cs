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

        // CREATE
        public async Task AddKlinikaAsync(KLINIKY klinika)
        {
            var sql = "BEGIN PROC_CREATE_KLINIKA(:Name, :Address, :Phone, :Email); END;";
            await _context.Database.ExecuteSqlRawAsync(sql,
                new OracleParameter("Name", klinika.NÁZEV),
                new OracleParameter("Address", klinika.ADRESA),
                new OracleParameter("Phone", klinika.TELEFONNÍ_ČÍSLO),
                new OracleParameter("Email", klinika.EMAIL));
        }

        // DELETE
        public async Task DeleteKlinikaAsync(int klinikaId)
        {
            var sql = "BEGIN PROC_DELETE_KLINIKA(:p_ID_Klinika); END;";
            var param = new OracleParameter("p_ID_Klinika", klinikaId);

            try
            {
                await _context.Database.ExecuteSqlRawAsync(sql, param);
            }
            catch (DbUpdateException ex)
            {
                // Ošetření chyby z databáze
                throw new Exception("Chyba při mazání kliniky: " + ex.InnerException?.Message);
            }
        }

        // PROC_CREATE_KLINIKA
        //PROC_EDIT_KLINIKA
    }
}