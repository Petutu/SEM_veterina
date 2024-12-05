using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using Sem_Veterina.Entity;

namespace Sem_Veterina.CRUD
{
    public class ZakrokService
    {
        private readonly OracleDbContext _context;

        public ZakrokService(OracleDbContext context)
        {
            _context = context;
        }

        // CREATE
        public async Task AddZakrokAsync(ZAKROK zakrok)
        {
            var sql = "BEGIN PROC_CREATE_ZAKROK(:ID_AKCE, :TYP_ZAKROK); END;";
            await _context.Database.ExecuteSqlRawAsync(sql,
                new OracleParameter("ID_AKCE", zakrok.ID_AKCE),
                new OracleParameter("TYP_ZAKROK", zakrok.TYP_ZAKROK));
        }

        // READ - GET ALL
        public async Task<List<ZAKROK>> GetAllZakrokyAsync()
        {
            var sql = "SELECT * FROM ZAKROK";
            return await _context.Zakroky.FromSqlRaw(sql).ToListAsync();
        }

        // READ - GET BY ID
        public async Task<ZAKROK> GetZakrokByIdAsync(int id)
        {
            var sql = "SELECT * FROM ZAKROK WHERE ID_AKCE = :Id";
            return await _context.Zakroky.FromSqlRaw(sql, new OracleParameter("Id", id)).FirstOrDefaultAsync();
        }

        // UPDATE
        public async Task UpdateZakrokAsync(ZAKROK zakrok)
        {
            var sql = "BEGIN PROC_EDIT_ZAKROK(:ID_AKCE, :TYP_ZAKROK); END;";
            await _context.Database.ExecuteSqlRawAsync(sql,
                new OracleParameter("ID_AKCE", zakrok.ID_AKCE),
                new OracleParameter("TYP_ZAKROK", zakrok.TYP_ZAKROK));
        }

        // DELETE
        public async Task DeleteZakrokAsync(int id)
        {
            var sql = "BEGIN PROC_DELETE_ZAKROK(:ID_AKCE); END;";
            await _context.Database.ExecuteSqlRawAsync(sql,
                new OracleParameter("ID_AKCE", id));
        }
    }
}
