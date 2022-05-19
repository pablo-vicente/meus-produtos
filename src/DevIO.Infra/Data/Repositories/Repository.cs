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
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {

        protected readonly MeuDbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        protected Repository()
        {
            _dbContext = new MeuDbContext();
            _dbSet = _dbContext.Set<TEntity>();
        }
        
        public virtual async Task<TEntity> ObterPorId(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<Entity>> ObterTodos()
        {
            return await _dbSet.ToListAsync();
        }
        
        public  async Task<IEnumerable<TEntity>> Buscar(Expression<Func<Entity, bool>> predicate)
        {
            return (IEnumerable<TEntity>) await _dbSet
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync();
        }
        
        public virtual async Task Adicionar(TEntity entity)
        {
            _dbSet.Add(entity);
            await SaveChanges();
        }
        public virtual async Task Atualizar(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await SaveChanges();
        }

        public virtual async Task Remover(Guid id)
        {
            _dbContext.Entry(new TEntity
            {
                Id = id
            }).State = EntityState.Deleted;
            
            await SaveChanges();
        }

        public virtual async Task<int> SaveChanges()
        {
            return await _dbContext.SaveChangesAsync();
        }
        
        public virtual void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}