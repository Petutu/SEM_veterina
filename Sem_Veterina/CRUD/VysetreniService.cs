using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using Sem_Veterina.Entity;

namespace Sem_Veterina.CRUD
{
    public class VysetreniService
    {
        private readonly OracleDbContext _context;

        public VysetreniService(OracleDbContext context)
        {
            _context = context;
        }

        // CREATE
        public async Task AddVysetreniAsync(VYSETRENI vysetreni)
        {
            var sql = "BEGIN PROC_CREATE_VYSETRENI(:ID_AKCE, :TYP_VYSETRENI); END;";
            await _context.Database.ExecuteSqlRawAsync(sql,
                new OracleParameter("ID_AKCE", vysetreni.ID_AKCE),
                new OracleParameter("TYP_VYSETRENI", vysetreni.TYP_VYSETRENI));
        }

        // READ - GET ALL
        public async Task<List<VYSETRENI>> GetAllVysetreniAsync()
        {
            var sql = "SELECT * FROM VYSETRENI";
            return await _context.Vysetreni.FromSqlRaw(sql).ToListAsync();
        }

        // READ - GET BY ID
        public async Task<VYSETRENI> GetVysetreniByIdAsync(int id)
        {
            var sql = "SELECT * FROM VYSETRENI WHERE ID_AKCE = :Id";
            return await _context.Vysetreni.FromSqlRaw(sql, new OracleParameter("Id", id)).FirstOrDefaultAsync();
        }

        // UPDATE
        public async Task UpdateVysetreniAsync(VYSETRENI vysetreni)
        {
            var sql = "BEGIN PROC_EDIT_VYSETRENI(:ID_AKCE, :TYP_VYSETRENI); END;";
            await _context.Database.ExecuteSqlRawAsync(sql,
                new OracleParameter("ID_AKCE", vysetreni.ID_AKCE),
                new OracleParameter("TYP_VYSETRENI", vysetreni.TYP_VYSETRENI));
        }

        // DELETE
        public async Task DeleteVysetreniAsync(int id)
        {
            var sql = "BEGIN PROC_DELETE_VYSETRENI(:ID_AKCE); END;";
            await _context.Database.ExecuteSqlRawAsync(sql,
                new OracleParameter("ID_AKCE", id));
        }
    }
}
