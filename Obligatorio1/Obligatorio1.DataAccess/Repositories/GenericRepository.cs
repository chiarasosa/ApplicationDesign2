using Microsoft.EntityFrameworkCore;
using Obligatorio1.DataAccess.Contexts;
using Obligatorio1.IDataAccess;
using System.Linq.Expressions;

namespace Obligatorio1.DataAccess.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected DbContext Context { get; }
        public GenericRepository(Context context)
        {
            Context = context;
        }

        public virtual void Insert(T entity)
        {
            Context.Set<T>().Add(entity);
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public virtual T Get(Expression<Func<T, bool>> searchCondition, List<string> includes = null)
        {
            IQueryable<T> query = Context.Set<T>();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return query.Where(searchCondition).Select(x => x).FirstOrDefault();
        }

        public virtual IEnumerable<U> GetAll<U>() where U : class
        {
            return Context.Set<U>().ToList();
        }
        public void Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }
    }
}