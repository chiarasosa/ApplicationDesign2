using System.Linq.Expressions;

namespace Obligatorio1.IDataAccess
{
    public interface IGenericRepository<T> where T : class
    {
        void Insert(T entity);
        IEnumerable<U> GetAll<U>() where U : class;

        T Get(Expression<Func<T, bool>> searchCondition, List<string> includes = null);
        void Save();
        void Delete(T entity);

        void Update(T entity);
    }
}
