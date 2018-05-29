using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace RankBoard.Repositories
{
    public abstract class BaseUnitOfWork
    {
        private readonly DbContext _context;

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
