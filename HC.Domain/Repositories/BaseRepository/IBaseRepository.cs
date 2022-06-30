using HC.Domain.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HC.Domain.Repositories.BaseRepository
{
    public interface IBaseRepository<T> where T : IBaseEntity
    {
        //Add
        Task<string> Add(T model);
        //Update
        Task<string> Update(T model);
        //Delete
        Task<string> Delete(Guid id);
        //List
        Task<List<T>> GetList();
        Task<T> GetById(Guid id);
        Task<List<T>> GetByDefaults(Expression<Func<T, bool>> expression);

        Task<bool> Any(Expression<Func<T, bool>> expression);
    }
}
