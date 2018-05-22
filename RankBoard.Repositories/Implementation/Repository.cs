using Microsoft.EntityFrameworkCore;
using RankBoard.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RankBoard.Repositories.Implementation
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {
        protected DbContext _context;
        private DbSet<TEntity> _set;

        public Repository(DbContext context)
        {
            _context = context;
        }

        protected DbSet<TEntity> Set
        {
            get { return _set ?? (_set = _context.Set<TEntity>()); }
        }

        public void Add(TEntity entity)
        {
            Set.Add(entity);
        }

        public TEntity FindById(TKey id)
        {
            return Set.Find(id);
        }

        public Task<TEntity> FindByIdAsync(TKey id)
        {
            return Set.FindAsync(id);
        }

        public Task<TEntity> FindByIdAsync(CancellationToken cancellationToken, TKey id)
        {
            return Set.FindAsync(id, cancellationToken);
        }

        public List<TEntity> GetAll()
        {
            return Set.ToList();
        }

        public Task<List<TEntity>> GetAllAsync()
        {
            return Set.ToListAsync();
        }

        public Task<List<TEntity>> GetAllAsync(CancellationToken cancelationToken)
        {
            return Set.ToListAsync(cancelationToken);
        }

        public List<TEntity> PageAll(int skip, int take)
        {
            return Set.Skip(skip).Take(take).ToList();
        }

        public Task<List<TEntity>> PageAllAsync(int skip, int take)
        {
            return Set.Skip(skip).Take(take).ToListAsync();
        }

        public Task<List<TEntity>> PageAllAsync(CancellationToken cancellationToken, int skip, int take)
        {
            return Set.Skip(skip).Take(take).ToListAsync(cancellationToken);
        }

        public void Remove(TEntity entity)
        {
            Set.Remove(entity);
        }

        public void Update(TEntity entity)
        {
            var entry = _context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                Set.Attach(entity);
                entry = _context.Entry(entity);
            }

            entry.State = EntityState.Modified;
        }
    }
}
