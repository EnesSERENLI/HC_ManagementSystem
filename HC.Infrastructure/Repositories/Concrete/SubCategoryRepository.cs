using HC.Domain.Entities.Concrete;
using HC.Domain.Repositories.BaseRepository;
using HC.Domain.Repositories.EntityTypeRepositoy;
using HC.Infrastructure.Context;
using HC.Infrastructure.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HC.Infrastructure.Repositories.Concrete
{
    public class SubCategoryRepository : BaseRepository<SubCategory>, ISubCategoryRepository
    {
        public SubCategoryRepository(HotCatDbContext db) : base(db)
        {
        }
    }
}
