using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using Sem_Veterina.Entity;

namespace Sem_Veterina.CRUD
{
    public class VysledkyKrveService
    {
        private readonly OracleDbContext _context;

        public VysledkyKrveService(OracleDbContext context)
        {
            _context = context;
        }

        // CREATE
        public async Task AddVysledekKrveAsync(VYSLEDKYKRVE vysledekKrve)
        {
            var sql = "BEGIN PROC_CREATE_VYSLEDKY_KRVE(:ID_AKCE, :TYP_TESTU); END;";
            await _context.Database.ExecuteSqlRawAsync(sql,
                new OracleParameter("ID_AKCE", vysledekKrve.ID_AKCE),
                new OracleParameter("TYP_TESTU", vysledekKrve.TYP_TESTU));
        }

        // READ - GET ALL
        public async Task<List<VYSLEDKYKRVE>> GetAllVysledkyKrveAsync()
        {
            var sql = "SELECT * FROM VYSLEDKY_KRVE";
            return await _context.VysledkyKrve.FromSqlRaw(sql).ToListAsync();
        }

        // READ - GET BY ID
        public async Task<VYSLEDKYKRVE> GetVysledekKrveByIdAsync(int id)
        {
            var sql = "SELECT * FROM VYSLEDKY_KRVE WHERE ID_AKCE = :Id";
            return await _context.VysledkyKrve.FromSqlRaw(sql, new OracleParameter("Id", id)).FirstOrDefaultAsync();
        }

        // UPDATE
        public async Task UpdateVysledekKrveAsync(VYSLEDKYKRVE vysledekKrve)
        {
            var sql = "BEGIN PROC_EDIT_VYSLEDKY_KRVE(:ID_AKCE, :TYP_TESTU); END;";
            await _context.Database.ExecuteSqlRawAsync(sql,
                new OracleParameter("ID_AKCE", vysledekKrve.ID_AKCE),
                new OracleParameter("TYP_TESTU", vysledekKrve.TYP_TESTU));
        }

        // DELETE
        public async Task DeleteVysledekKrveAsync(int id)
        {
            var sql = "BEGIN PROC_DELETE_VYSLEDKY_KRVE(:ID_AKCE); END;";
            await _context.Database.ExecuteSqlRawAsync(sql,
                new OracleParameter("ID_AKCE", id));
        }
    }
}
