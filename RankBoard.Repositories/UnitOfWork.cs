using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RankBoard.Repositories
{
    public class UnitOfWork : BaseUnitOfWork, IUnitOfWork, IDisposable
    {
        private readonly DbContext _context;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }
    }
}
