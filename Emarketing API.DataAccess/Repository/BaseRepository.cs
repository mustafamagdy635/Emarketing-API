using Emarketing_API.DataAccess.Data;
using Emarketing_API.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Emarketing_API.DataAccess.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly Context _db;

        private readonly DbSet<T> _Dbset;
        public BaseRepository(Context db)
        {
            this._db = db;
            this._Dbset = _db.Set<T>();
        }
        public void Add(T entity)
        {
            _Dbset.Add(entity);
        }
        public void Delete(T entity)
        {
            _Dbset.Remove(entity);
        }
        public T Find(Expression<Func<T, bool>> filter, string[]? IncludeProperties = null)
        {
            IQueryable<T> Query = _Dbset;
            if (filter != null)
            {
                Query = Query.Where(filter);
            }
            if (IncludeProperties != null)
            {
                foreach (var Properties in IncludeProperties)
                {
                    var propertyInfo = typeof(T).GetProperty(Properties);
                    if (propertyInfo != null)
                    {
                        Query = Query.Include(Properties);
                    }
                    else
                    {
                        throw new InvalidOperationException($"Property '{Properties}' does not exist on entity type '{typeof(T).Name}'");
                    }

                }
            }
            return Query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string[]? IncludeProperties = null)
        {
            IQueryable<T> Query = _Dbset;
            if (filter != null)
            {
                Query = Query.Where(filter);
            }
            if (IncludeProperties != null)
            {
                foreach (var Properties in IncludeProperties)
                {
                    var propertyInfo = typeof(T).GetProperty(Properties);

                    if (propertyInfo != null)
                    {
                        Query = Query.Include(Properties);
                    }
                    else
                    {
                        throw new InvalidOperationException($"Property '{Properties}' does not exist on entity type '{typeof(T).Name}'");
                    }
                }
            }
            return Query.ToList();
        }
        public void Update(T entity)
        {
            _Dbset.Update(entity);
        }
    }
}
