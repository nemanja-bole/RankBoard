using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RankBoard.Repositories.Interface
{
    public interface IRepository<TEntity, TKey> where TEntity : class
    {
        List<TEntity> GetAll();
        Task<List<TEntity>> GetAllAsync();
        Task<List<TEntity>> GetAllAsync(CancellationToken cancelationToken);

        List<TEntity> PageAll(int skip, int take);
        Task<List<TEntity>> PageAllAsync(int skip, int take);
        Task<List<TEntity>> PageAllAsync(CancellationToken cancellationToken, int skip, int take);

        TEntity FindById(TKey id);
        Task<TEntity> FindByIdAsync(TKey id);
        Task<TEntity> FindByIdAsync(CancellationToken cancellationToken, TKey id);

        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);

        void Remove(TKey key); 
    }
}
