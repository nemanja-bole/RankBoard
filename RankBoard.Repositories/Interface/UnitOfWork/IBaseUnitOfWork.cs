using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RankBoard.Repositories.Interface.UnitOfWork
{
    public interface IBaseUnitOfWork : IDisposable
    {
        int SaveChanges();

        Task<int> SaveChangesAsync();

        Task<int> SaveChangesAsync(CancellationToken cancelationToken);
    }
}
