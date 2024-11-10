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
            var sql = "INSERT INTO PERSONAL (ID_PRESONÁL, JMÉNO, PŘÍJMENÍ, SPECIALIZACE, TELEFONNÍ_ČÍSLO, ADRESA, ID_KLINIKA) " +
                      "VALUES (:Id, :Jmeno, :Prijmeni, :Specializace, :Telefon, :Adresa, :IdKlinika)";
            await _context.Database.ExecuteSqlRawAsync(sql,
                new OracleParameter("Id", personal.ID_PRESONÁL),
                new OracleParameter("Jmeno", personal.JMÉNO),
                new OracleParameter("Prijmeni", personal.PŘÍJMENÍ),
                new OracleParameter("Specializace", personal.SPECIALIZACE),
                new OracleParameter("Telefon", personal.TELEFONNÍ_ČÍSLO ?? (object)DBNull.Value),
                new OracleParameter("Adresa", personal.ADRESA ?? (object)DBNull.Value),
                new OracleParameter("IdKlinika", personal.ID_KLINIKA));
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
            var sql = "UPDATE PERSONAL SET JMÉNO = :Jmeno, PŘÍJMENÍ = :Prijmeni, SPECIALIZACE = :Specializace, " +
                      "TELEFONNÍ_ČÍSLO = :Telefon, ADRESA = :Adresa, ID_KLINIKA = :IdKlinika WHERE ID_PRESONÁL = :Id";
            await _context.Database.ExecuteSqlRawAsync(sql,
                new OracleParameter("Jmeno", personal.JMÉNO),
                new OracleParameter("Prijmeni", personal.PŘÍJMENÍ),
                new OracleParameter("Specializace", personal.SPECIALIZACE),
                new OracleParameter("Telefon", personal.TELEFONNÍ_ČÍSLO ?? (object)DBNull.Value),
                new OracleParameter("Adresa", personal.ADRESA ?? (object)DBNull.Value),
                new OracleParameter("IdKlinika", personal.ID_KLINIKA),
                new OracleParameter("Id", personal.ID_PRESONÁL));
        }

        // DELETE
        public async Task DeletePersonalAsync(int id)
        {
            var sql = "DELETE FROM PERSONAL WHERE ID_PRESONÁL = :Id";
            await _context.Database.ExecuteSqlRawAsync(sql, new OracleParameter("Id", id));
        }
    }
}
