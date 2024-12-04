using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using Sem_Veterina.Entity;

namespace Sem_Veterina.CRUD
{
    public class PersonalService
    {
        private readonly OracleDbContext _context;

        public PersonalService(OracleDbContext context)
        {
            _context = context;
        }

        // CREATE
        public async Task AddPersonalAsync(PERSONAL personal)
        {
            var sql = "BEGIN PROC_CREATE_PERSONAL(:Jmeno, :Prijmeni, :Specializace, :TelefonniCislo, :Adresa, :IdKlinika, :IdNadrizeny, :IdUzivatel); END;";
            await _context.Database.ExecuteSqlRawAsync(sql,
                new OracleParameter("Jmeno", personal.JMÉNO),
                new OracleParameter("Prijmeni", personal.PŘÍJMENÍ),
                new OracleParameter("Specializace", personal.SPECIALIZACE),
                new OracleParameter("TelefonniCislo", personal.TELEFONNÍ_ČÍSLO ?? (object)DBNull.Value),
                new OracleParameter("Adresa", personal.ADRESA ?? (object)DBNull.Value),
                new OracleParameter("IdKlinika", personal.ID_KLINIKA),
                new OracleParameter("IdNadrizeny", personal.ID_NADRIZENY ?? (object)DBNull.Value),
                new OracleParameter("IdUzivatel", personal.ID_UZIVATEL ?? (object)DBNull.Value));
        }


        // READ - GET ALL
        public async Task<List<PERSONAL>> GetAllPersonalAsync()
        {
            var sql = "SELECT * FROM PERSONAL";
            return await _context.Personal.FromSqlRaw(sql).ToListAsync();
        }

        // READ - GET BY ID
        public async Task<PERSONAL> GetPersonalByIdAsync(int id)
        {
            var sql = "SELECT * FROM PERSONAL WHERE ID_PRESONÁL = :Id";
            var param = new OracleParameter("Id", id);
            return await _context.Personal.FromSqlRaw(sql, param).FirstOrDefaultAsync();
        }

        // UPDATE
        public async Task UpdatePersonalAsync(PERSONAL personal)
        {
            var sql = "BEGIN PROC_EDIT_PERSONAL(:IdPersonal, :Jmeno, :Prijmeni, :Specializace, :TelefonniCislo, :Adresa, :IdKlinika, :IdNadrizeny, :IdUzivatel); END;";
            await _context.Database.ExecuteSqlRawAsync(sql,
                new OracleParameter("IdPersonal", personal.ID_PRESONÁL),
                new OracleParameter("Jmeno", personal.JMÉNO),
                new OracleParameter("Prijmeni", personal.PŘÍJMENÍ),
                new OracleParameter("Specializace", personal.SPECIALIZACE),
                new OracleParameter("TelefonniCislo", personal.TELEFONNÍ_ČÍSLO ?? (object)DBNull.Value),
                new OracleParameter("Adresa", personal.ADRESA ?? (object)DBNull.Value),
                new OracleParameter("IdKlinika", personal.ID_KLINIKA),
                new OracleParameter("IdNadrizeny", personal.ID_NADRIZENY ?? (object)DBNull.Value),
                new OracleParameter("IdUzivatel", personal.ID_UZIVATEL ?? (object)DBNull.Value));
        }


        // DELETE
        public async Task DeletePersonalAsync(int idPersonal)
        {
            var sql = "BEGIN PROC_DELETE_PERSONAL(:IdPersonal); END;";
            await _context.Database.ExecuteSqlRawAsync(sql,
                new OracleParameter("IdPersonal", idPersonal));
        }


        public async Task<List<PERSONAL>> GetFilteredPersonalAsync(string? name, string? surname, string? specialization, int? klinikaId)
        {
            var sql = "SELECT * FROM PERSONAL WHERE (:Name IS NULL OR JMÉNO LIKE '%' || :Name || '%') " +
                      "AND (:Surname IS NULL OR PŘÍJMENÍ LIKE '%' || :Surname || '%') " +
                      "AND (:Specialization IS NULL OR SPECIALIZACE LIKE '%' || :Specialization || '%') " +
                      "AND (:KlinikaId IS NULL OR ID_KLINIKA = :KlinikaId)";

            var parameters = new[]
            {
        new OracleParameter("Name", name ?? (object)DBNull.Value),
        new OracleParameter("Surname", surname ?? (object)DBNull.Value),
        new OracleParameter("Specialization", specialization ?? (object)DBNull.Value),
        new OracleParameter("KlinikaId", klinikaId ?? (object)DBNull.Value)
    };

            return await _context.Personal.FromSqlRaw(sql, parameters).ToListAsync();
        }
    }
}
