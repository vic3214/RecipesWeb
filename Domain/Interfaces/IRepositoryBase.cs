using System.Linq.Expressions;

namespace Domain.Interfaces
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll(List<string> include);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, List<string> include);
        IQueryable<T> FindByConditionWithTracking(Expression<Func<T, bool>> expression, List<string> include);
        int Create(T entity);
        void Update(T entity);
        void UpdateById(int id, T entity);
        void Delete(T entity);
        void Delete(int id);
        void RemoveRange(IEnumerable<T> entities);
    }
}
