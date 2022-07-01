using HC.Domain.Entities.Concrete;
using HC.Infrastructure.Mapping.Concrete;
using HC.Infrastructure.SeedData;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Infrastructure.Context
{
    public class HotCatDbContext : IdentityDbContext<AppUser>
    {
        public HotCatDbContext(DbContextOptions options)
            :base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppUserRole> AppUserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProductMap());
            builder.ApplyConfiguration(new CategoryMap());
            builder.ApplyConfiguration(new SubCategoryMap());
            builder.ApplyConfiguration(new EmployeeMap());
            builder.ApplyConfiguration(new OrderDetailMap());
            builder.ApplyConfiguration(new OrderMap());
            builder.ApplyConfiguration(new DepartmentMap());

            builder.ApplyConfiguration(new DepartmentSeeding());
            builder.ApplyConfiguration(new AppUserMap());

            base.OnModelCreating(builder);
        }
    }
}
