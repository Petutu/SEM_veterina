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
    }
}