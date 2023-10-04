using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Obligatorio1.DataAccess.Contexts;
using Obligatorio1.IDataAccess;

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
