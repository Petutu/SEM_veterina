using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using Sem_Veterina.Entity;

namespace Sem_Veterina.CRUD
{
    public class UzivatelService
    {
        private readonly OracleDbContext _context;

        public UzivatelService(OracleDbContext context)
        {
            _context = context;
        }

        // CREATE
        public async Task AddUzivatelAsync(UZIVATEL uzivatel)
        {
            var sql = "INSERT INTO UZIVATEL (ID_UZIVATEL, USERNAME, HESLO, ID_ROLE) " +
                      "VALUES (null, :Username, :Heslo, :Role)";
            await _context.Database.ExecuteSqlRawAsync(sql,
                // new OracleParameter("Id", uzivatel.ID_UZIVATEL),
                new OracleParameter("Username", uzivatel.USERNAME),
                new OracleParameter("Heslo", uzivatel.HESLO),
                new OracleParameter("Role", uzivatel.ID_ROLE));
        }

        // READ - GET ALL
        public async Task<List<UZIVATEL>> GetAllUzivateleAsync()
        {
            var sql = "SELECT * FROM UZIVATEL";
            return await _context.Uzivatele.FromSqlRaw(sql).ToListAsync();
        }

        // READ - GET BY USERNAME
        public async Task<UZIVATEL> GetUzivatelByUsernameAsync(string username)
        {
            var sql = "SELECT * FROM UZIVATEL WHERE USERNAME = :Username";
            var param = new OracleParameter("Username", username);
            return await _context.Uzivatele.FromSqlRaw(sql, param).FirstOrDefaultAsync();
        }
    }
}