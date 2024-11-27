using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using Sem_Veterina.Entity;

namespace Sem_Veterina.CRUD
{
    public class LogovaniService
    {
        private readonly OracleDbContext _context;

        public LogovaniService(OracleDbContext context)
        {
            _context = context;
        }
    }
}