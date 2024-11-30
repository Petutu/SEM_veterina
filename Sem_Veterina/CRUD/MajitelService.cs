using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using Sem_Veterina.Entity;

namespace Sem_Veterina.CRUD
{
    public class MajitelService
    {
        private readonly OracleDbContext _context;

        public MajitelService(OracleDbContext context)
        {
            _context = context;
        }

        // CREATE
        public async Task AddMajitelAsync(MAJITELE majitel)
        {
            var sql = "INSERT INTO MAJITELI (ID_MAJITEL, JMÉNO, PŘÍJMENÍ, TELEFONNÍ_ČÍSLO, ADRESA, EMAIL, ID_KLINIKA) " +
                      "VALUES (:Id, :Jmeno, :Prijmeni, :Telefon, :Adresa, :Email, :IdKlinika)";
            await _context.Database.ExecuteSqlRawAsync(sql,
                new OracleParameter("Id", majitel.ID_MAJITEL),
                new OracleParameter("Jmeno", majitel.JMÉNO),
                new OracleParameter("Prijmeni", majitel.PŘÍJMENÍ),
                new OracleParameter("Telefon", majitel.TELEFONNÍ_ČÍSLO),
                new OracleParameter("Adresa", majitel.ADRESA),
                new OracleParameter("Email", majitel.EMAIL ?? (object)DBNull.Value),
                new OracleParameter("IdKlinika", majitel.ID_KLINIKA));
        }

        // READ - GET ALL
        public async Task<List<MAJITELE>> GetAllMajiteleAsync()
        {
            var sql = "SELECT * FROM MAJITELI";
            return await _context.Majitele.FromSqlRaw(sql).ToListAsync();
        }

        // READ - GET BY ID
        public async Task<MAJITELE> GetMajitelByIdAsync(int id)
        {
            var sql = "SELECT * FROM MAJITELI WHERE ID_MAJITEL = :Id";
            var param = new OracleParameter("Id", id);
            return await _context.Majitele.FromSqlRaw(sql, param).FirstOrDefaultAsync();
        }

        // FILTERED READ
        public async Task<List<MAJITELE>> GetFilteredMajiteleAsync(string? name, string? lastname, string? phone)
        {
            var query = _context.Majitele.AsQueryable();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(m => m.JMÉNO.Contains(name));
            if (!string.IsNullOrEmpty(lastname))
                query = query.Where(m => m.PŘÍJMENÍ.Contains(lastname));
            if (!string.IsNullOrEmpty(phone))
                query = query.Where(m => m.TELEFONNÍ_ČÍSLO.ToString().Contains(phone));

            return await query.ToListAsync();
        }

        // UPDATE
        public async Task UpdateMajitelAsync(MAJITELE majitel)
        {
            var sql = "UPDATE MAJITELI SET JMÉNO = :Jmeno, PŘÍJMENÍ = :Prijmeni, TELEFONNÍ_ČÍSLO = :Telefon, " +
                      "ADRESA = :Adresa, EMAIL = :Email, ID_KLINIKA = :IdKlinika WHERE ID_MAJITEL = :Id";
            await _context.Database.ExecuteSqlRawAsync(sql,
                new OracleParameter("Jmeno", majitel.JMÉNO),
                new OracleParameter("Prijmeni", majitel.PŘÍJMENÍ),
                new OracleParameter("Telefon", majitel.TELEFONNÍ_ČÍSLO),
                new OracleParameter("Adresa", majitel.ADRESA),
                new OracleParameter("Email", majitel.EMAIL ?? (object)DBNull.Value),
                new OracleParameter("IdKlinika", majitel.ID_KLINIKA),
                new OracleParameter("Id", majitel.ID_MAJITEL));
        }

        // DELETE
        public async Task DeleteMajitelAsync(int id)
        {
            var sql = "DELETE FROM MAJITELI WHERE ID_MAJITEL = :Id";
            await _context.Database.ExecuteSqlRawAsync(sql, new OracleParameter("Id", id));
        }

        // EXISTENCE CHECK
        public async Task<bool> MajitelExistsAsync(int id)
        {
            return await _context.Majitele.AnyAsync(m => m.ID_MAJITEL == id);
        }
    }
}