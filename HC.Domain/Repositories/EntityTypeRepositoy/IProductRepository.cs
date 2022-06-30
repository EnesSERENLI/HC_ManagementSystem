using HC.Domain.Entities.Concrete;
using HC.Domain.Repositories.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Domain.Repositories.EntityTypeRepositoy
{
    public interface IProductRepository : IBaseRepository<Product>
    {
    }
}
