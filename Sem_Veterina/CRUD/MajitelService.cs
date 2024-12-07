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
            var sql = "BEGIN PROC_CREATE_MAJITEL(:Jmeno, :Prijmeni, :TelefonniCislo, :Adresa, :Email, :IdKlinika, :IdUzivatel); END;";
            await _context.Database.ExecuteSqlRawAsync(sql,
                new OracleParameter("Jmeno", majitel.JMÉNO),
                new OracleParameter("Prijmeni", majitel.PŘÍJMENÍ),
                new OracleParameter("TelefonniCislo", majitel.TELEFONNÍ_ČÍSLO),
                new OracleParameter("Adresa", majitel.ADRESA),
                new OracleParameter("Email", majitel.EMAIL ?? (object)DBNull.Value),
                new OracleParameter("IdKlinika", majitel.ID_KLINIKA),
                new OracleParameter("IdUzivatel", majitel.ID_UZIVATEL ?? (object)DBNull.Value));
        }


        // READ - GET ALL
        public async Task<List<MAJITELE>> GetAllMajiteleAsync()
        {
            var sql = "SELECT * FROM MAJITELI";
            return await _context.Majitele.FromSqlRaw(sql).ToListAsync();
        }

        // READ - GET BY ID
        public async Task<MajitelDetailView> GetMajitelByIdAsync(int id)
        {
            var sql = "SELECT * FROM V_MAJITEL_DETAILS WHERE ID_MAJITEL = :Id";
            var param = new OracleParameter("Id", id);
            return await _context.MajitelDetailView.FromSqlRaw(sql, param).FirstOrDefaultAsync();
        }

        // FILTERED READ
        public async Task<List<MajitelDetailView>> GetFilteredMajiteleAsync(string? name, string? lastname, string? phone)
        {
            var sql = "SELECT * FROM V_MAJITEL_DETAILS WHERE (:Name IS NULL OR JMÉNO_MAJITELE LIKE '%' || :Name || '%') " +
                      "AND (:Surname IS NULL OR PŘÍJMENÍ_MAJITELE LIKE '%' || :Surname || '%')" +
                      "AND (:Phone IS NULL OR TELEFONNÍ_ČÍSLO LIKE '%' || :Phone || '%')";
            var parameters = new[]
  {
                new OracleParameter("Name", name ?? (object)DBNull.Value),
                new OracleParameter("Surname", lastname ?? (object)DBNull.Value),
                new OracleParameter("Phone", phone ?? (object)DBNull.Value)
            };
            return await _context.MajitelDetailView.FromSqlRaw(sql, parameters).ToListAsync();
        }

        // UPDATE
        public async Task UpdateMajitelAsync(MAJITELE majitel)
        {
            var sql = "BEGIN PROC_EDIT_MAJITEL(:IdMajitel, :Jmeno, :Prijmeni, :TelefonniCislo, :Adresa, :Email, :IdKlinika, :IdUzivatel); END;";
            await _context.Database.ExecuteSqlRawAsync(sql,
                new OracleParameter("IdMajitel", majitel.ID_MAJITEL),
                new OracleParameter("Jmeno", majitel.JMÉNO),
                new OracleParameter("Prijmeni", majitel.PŘÍJMENÍ),
                new OracleParameter("TelefonniCislo", majitel.TELEFONNÍ_ČÍSLO),
                new OracleParameter("Adresa", majitel.ADRESA),
                new OracleParameter("Email", majitel.EMAIL ?? (object)DBNull.Value),
                new OracleParameter("IdKlinika", majitel.ID_KLINIKA),
                new OracleParameter("IdUzivatel", majitel.ID_UZIVATEL ?? (object)DBNull.Value));
        }


        // DELETE
        public async Task DeleteMajitelAsync(int idMajitel)
        {
            var sql = "BEGIN PROC_DELETE_MAJITEL(:IdMajitel); END;";
            await _context.Database.ExecuteSqlRawAsync(sql,
                new OracleParameter("IdMajitel", idMajitel));
        }


        // EXISTENCE CHECK
        public async Task<bool> MajitelExistsAsync(int id)
        {
            return await _context.Majitele.AnyAsync(m => m.ID_MAJITEL == id);
        }
    }
}