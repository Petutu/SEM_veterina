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
            var sql = "BEGIN PROC_CREATE_UZIVATEL(:Username, :Heslo, :Role); END;";
            await _context.Database.ExecuteSqlRawAsync(sql,
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
         public async Task<UZIVATEL> GetUzivatelByIdAsync(int id)
        {
            var sql = "SELECT * FROM UZIVATEL WHERE ID_UZIVATEL = :Id";
            var param = new OracleParameter("Id", id);
            return await _context.Uzivatele.FromSqlRaw(sql, param).FirstOrDefaultAsync();
        }

        public async Task UpdateUzivatelAsync(UZIVATEL uzivatel)
        {
            var sql = "BEGIN PROC_EDIT_UZIVATEL(:IdUzivatel, :Username, :Heslo, :Role); END;";
            await _context.Database.ExecuteSqlRawAsync(sql,
                new OracleParameter("IdUzivatel", uzivatel.ID_UZIVATEL),
                new OracleParameter("Username", uzivatel.USERNAME),
                new OracleParameter("Heslo", uzivatel.HESLO),
                new OracleParameter("Role", uzivatel.ID_ROLE));
        }


        // DELETE
        public async Task DeleteUzivatelAsync(int idUzivatel)
        {
            var sql = "BEGIN PROC_DELETE_UZIVATEL(:IdUzivatel); END;";
            await _context.Database.ExecuteSqlRawAsync(sql,
                new OracleParameter("IdUzivatel", idUzivatel));
        }


        public async Task<List<UZIVATEL>> GetFilteredUzivateleAsync(string? username, string? roleName)
        {
            var sql = "SELECT u.* FROM UZIVATEL u LEFT JOIN ROLE r ON u.ID_ROLE = r.ID_ROLE " +
                      "WHERE (:Username IS NULL OR u.USERNAME LIKE '%' || :Username || '%') " +
                      "AND (:RoleName IS NULL OR r.NAZEV_ROLE LIKE '%' || :RoleName || '%')";

            var parameters = new[]
            {
        new OracleParameter("Username", username ?? (object)DBNull.Value),
        new OracleParameter("RoleName", roleName ?? (object)DBNull.Value)
    };

            return await _context.Uzivatele.FromSqlRaw(sql, parameters).ToListAsync();
        }

    }
}