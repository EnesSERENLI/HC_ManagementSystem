using HC.Domain.Entities.Interface;
using HC.Domain.Repositories.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HC.Infrastructure.Repositories.Abstract
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class, IBaseEntity
    {
        public Task<string> Add(T model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Any(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<string> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetByDefaults(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetList()
        {
            throw new NotImplementedException();
        }

        public Task<string> Update(T model)
        {
            throw new NotImplementedException();
        }
    }
}
