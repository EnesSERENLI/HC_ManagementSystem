using HC.Domain.Repositories.EntityTypeRepositoy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Domain.UnitOfWork
{
    public interface IUnitOfWork
    {
        IAppUserRepository AppUserRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        ISubCategoryRepository SubCategoryRepository { get; }
        IEmployeeRepository EmployeeRepository { get; }
        IDepartmentRepository DepartmentRepository { get; }
        IProductRepository ProductRepository { get; }
        IRoleRepository RoleRepository { get; }
        IOrderRepository OrderRepository { get; }
        IOrderDetailRepository OrderDetailRepository { get; }
        Task Approve(); //for SaveChanges

    }
}
