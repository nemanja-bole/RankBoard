using Microsoft.EntityFrameworkCore;
using RankBoard.Repositories.Interface.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RankBoard.Repositories.Implementation.UnitOfWork
{
    public class UnitOfWork : BaseUnitOfWork, IUnitOfWork, IDisposable
    {
        public UnitOfWork(DbContext context)
        {
            _context = context;
        }
    }
}
