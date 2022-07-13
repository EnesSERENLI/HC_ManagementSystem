using HC.Domain.Repositories.EntityTypeRepositoy;
using HC.Domain.UnitOfWork;
using HC.Infrastructure.Context;
using HC.Infrastructure.Repositories.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HotCatDbContext _db;

        public UnitOfWork(HotCatDbContext db)
        {
            _db = db;
        }
        private IAppUserRepository _appUserRepository;
        public IAppUserRepository AppUserRepository
        {
            get
            {
                if (_appUserRepository == null)
                {
                    _appUserRepository = new AppUserRepository(_db);
                }
                return _appUserRepository;
            }
        }
        private ICategoryRepository _categoryRepository;
        public ICategoryRepository CategoryRepository
        {
            get
            {
                if (_categoryRepository == null)
                {
                    _categoryRepository = new CategoryRepository(_db);
                }
                return _categoryRepository;
            }
        }
        private ISubCategoryRepository _subCategoryRepository;
        public ISubCategoryRepository SubCategoryRepository
        {
            get
            {
                if (_subCategoryRepository == null)
                {
                    _subCategoryRepository = new SubCategoryRepository(_db);
                }
                return _subCategoryRepository;
            }
        }
        private IEmployeeRepository _employeeRepository;
        public IEmployeeRepository EmployeeRepository
        {
            get
            {
                if (_employeeRepository == null)
                {
                    _employeeRepository = new EmployeeRepository(_db);
                }
                return _employeeRepository;
            }
        }
        private IDepartmentRepository _departmentRepository;
        public IDepartmentRepository DepartmentRepository
        {
            get
            {
                if (_departmentRepository == null)
                {
                    _departmentRepository = new DepartmentRepository(_db);
                }
                return _departmentRepository;
            }
        }
        private IProductRepository _productRepository;
        public IProductRepository ProductRepository
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository = new ProductRepository(_db);
                }
                return _productRepository;
            }
        }

        public async Task Approve()
        {
            await _db.SaveChangesAsync();
        }

    }
}
