using InternalDAL.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InternalDAL.Repositories.Abstract
{
    public interface IRepositoryBase<T, Y> where T: BaseEntity<Y>
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        IQueryable<T> FindByConditionWithTracking(Expression<Func<T, bool>> expression);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<T> FindAsync(Y id);
        Task SaveAsync();
    }
}
