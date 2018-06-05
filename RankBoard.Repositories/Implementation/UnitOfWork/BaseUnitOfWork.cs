using Microsoft.EntityFrameworkCore;
using RankBoard.Repositories.Interface.UnitOfWork;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RankBoard.Repositories.Implementation.UnitOfWork
{
    public abstract class BaseUnitOfWork : IBaseUnitOfWork
    {
        protected DbContext _context;

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancelationToken)
        {
            return _context.SaveChangesAsync(cancelationToken);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
