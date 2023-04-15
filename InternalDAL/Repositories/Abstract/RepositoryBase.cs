using InternalDAL.Models.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InternalDAL.Repositories.Abstract
{
    public abstract class RepositoryBase<T, Y> : IRepositoryBase<T, Y> where T : BaseEntity<Y>
    {
        protected HubspotDataDbContext _hubspotDbContext { get; set; }
        public RepositoryBase(HubspotDataDbContext hubspotDbContext)
        {
            _hubspotDbContext = hubspotDbContext;
        }
        public IQueryable<T> FindAll() => _hubspotDbContext.Set<T>().AsNoTracking();
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
            _hubspotDbContext.Set<T>().Where(expression).AsNoTracking();
        public IQueryable<T> FindByConditionWithTracking(Expression<Func<T, bool>> expression) =>
            _hubspotDbContext.Set<T>().Where(expression);
        public void Create(T entity) => _hubspotDbContext.Set<T>().Add(entity);
        public void Update(T entity) => _hubspotDbContext.Set<T>().Update(entity);
        public void Delete(T entity) => _hubspotDbContext.Set<T>().Remove(entity);
        public async Task<T> FindAsync(Y id) => await _hubspotDbContext.Set<T>().FindAsync(id);
        public async Task SaveAsync() => await _hubspotDbContext.SaveChangesAsync();

        protected bool _disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _hubspotDbContext.Dispose();
                }
                _disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
