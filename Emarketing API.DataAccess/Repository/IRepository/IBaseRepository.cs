using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Emarketing_API.DataAccess.Repository.IRepository
{
    public interface IBaseRepository<T> where T : class
    {
        public  IEnumerable<T> GetAll(Expression<Func<T, bool>> ? filter = null, string[]? IncludeProperties = null);
        public T Find(Expression<Func<T, bool>> filter, string[]? IncludeProperties = null);
        public void Add(T entity);
        public void Update(T entity);
        public void Delete(T entity);

    }
}
