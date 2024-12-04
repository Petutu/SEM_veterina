using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using Sem_Veterina.Entity;


namespace Sem_Veterina.CRUD
{
    public class RoleService
    {
        private readonly OracleDbContext _context;

        public RoleService(OracleDbContext context)
        {
            _context = context;
        }

        // CREATE
        public async Task AddRoleAsync(ROLE role)
        {
            var sql = "BEGIN PROC_CREATE_ROLE(:NazevRole); END;";
            await _context.Database.ExecuteSqlRawAsync(sql,
                new OracleParameter("NazevRole", role.NAZEV_ROLE));
        }


        // READ - GET ALL
        public async Task<List<ROLE>> GetAllRoleAsync()
        {
            var sql = "SELECT * FROM ROLE";
            return await _context.Role.FromSqlRaw(sql).ToListAsync();
        }

        // READ - GET BY ID
        public async Task<ROLE> GetRoleByIdAsync(int id)
        {
            var sql = "SELECT * FROM ROLE WHERE ID_ROLE = :IdRole";
            var param = new OracleParameter("IdRole", id);
            return await _context.Role.FromSqlRaw(sql, param).FirstOrDefaultAsync();
        }

        // UPDATE
        public async Task UpdateRoleAsync(ROLE role)
        {
            var sql = "BEGIN PROC_EDIT_ROLE(:IdRole, :NazevRole); END;";
            await _context.Database.ExecuteSqlRawAsync(sql,
                new OracleParameter("IdRole", role.ID_ROLE),
                new OracleParameter("NazevRole", role.NAZEV_ROLE));
        }


        // DELETE
        public async Task DeleteRoleAsync(int idRole)
        {
            var sql = "BEGIN PROC_DELETE_ROLE(:IdRole); END;";
            await _context.Database.ExecuteSqlRawAsync(sql,
                new OracleParameter("IdRole", idRole));
        }


    }
}