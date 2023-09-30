using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio1.IDataAccess
{
    public interface IGenericRepository<T> where T : class
    {
        void Insert(T entity);

        IEnumerable<U> GetAll<U>() where U : class;

        void Save();
    }


}
