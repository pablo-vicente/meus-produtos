using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DevIO.Bussiness.Core.Data;
using DevIO.Bussiness.Core.Models;
using DevIO.Infra.Data.Context;

namespace DevIO.Infra.Data.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {

        protected readonly MeuDbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        protected Repository()
        {
            _dbContext = new MeuDbContext();
            _dbSet = _dbContext.Set<TEntity>();
        }
        
        public virtual async Task<TEntity> ObterPorIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<Entity>> ObterTodosAsync()
        {
            return await _dbSet.ToListAsync();
        }
        
        public  async Task<IEnumerable<TEntity>> BuscarAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync();
        }
        
        public virtual async Task AdicionarAsync(TEntity entity)
        {
            _dbSet.Add(entity);
            await SaveChangesAsync();
        }
        public virtual async Task AtualizarAsync(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await SaveChangesAsync();
        }

        public virtual async Task RemoverAsync(Guid id)
        {
            _dbContext.Entry(new TEntity
            {
                Id = id
            }).State = EntityState.Deleted;
            
            await SaveChangesAsync();
        }

        public virtual async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
        
        public virtual void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}