using HC.Domain.Entities.Interface;
using HC.Domain.Repositories.BaseRepository;
using HC.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
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
        private readonly HotCatDbContext _db;
        protected DbSet<T> table;
        public BaseRepository(HotCatDbContext db)
        {
            _db = db;
            table = _db.Set<T>();
        }
        public async Task<string> Add(T model)
        {
            await table.AddAsync(model);
            _db.SaveChanges();
            return "Data added";
        }

        public async Task<bool> Any(Expression<Func<T, bool>> expression) => await table.AnyAsync(expression);

        public async Task<string> Delete(Guid id)
        {
            try
            {
                T deleted = await GetById(id);
                deleted.Status = Domain.Enums.Status.Deleted;
                Update(deleted);
                return "Data deleted!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<List<T>> GetByDefaults(Expression<Func<T, bool>> expression) => await table.Where(expression).ToListAsync();

        public async Task<T> GetById(Guid id) =>await table.FindAsync(id);

        public async Task<TResult> GetFilteredFirstOrDefault<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> expression, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = table;
            if (include != null)
            {
                query = include(query);
            }
            if (expression != null)
            {
                query = query.Where(expression);
            }
            if (orderBy != null)
            {
                return await orderBy(query).Select(selector).FirstOrDefaultAsync();
            }
            else
            {
                return await query.Select(selector).FirstOrDefaultAsync();
            }
        }

        public async Task<List<TResult>> GetFilteredFirstOrDefaults<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> expression, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = table;
            if (include != null)
            {
                query = include(query);
            }
            if (expression != null)
            {
                query = query.Where(expression);
            }
            if (orderBy != null)
            {
                return await orderBy(query).Select(selector).ToListAsync();
            }
            else
            {
                return await query.Select(selector).ToListAsync();
            }
        }

        public Task<List<T>> GetList() => table.Cast<T>().ToListAsync();

        public async Task<string> Update(T model)
        {
            try
            {
                //T updated = await GetById(model.ID);
                //_db.Entry(updated).CurrentValues.SetValues(model);
                _db.Entry<T>(model).State = EntityState.Modified;
                _db.SaveChanges();

                return "Data Updated";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
