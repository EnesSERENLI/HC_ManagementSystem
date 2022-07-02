using HC.Application.Extensions;
using HC.Domain.Entities.Concrete;
using HC.Domain.Entities.Interface;
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

        public override int SaveChanges()
        {
            var modifiedEntities = ChangeTracker.Entries()
                .Where(x=>x.State == EntityState.Added ||
                       x.State == EntityState.Modified ||
                       x.State == EntityState.Deleted).ToList();

            string computerName = Environment.MachineName;

            string ipAddress = RemoteIpAddress.GetIpAddress();

            DateTime date = DateTime.Now;

            foreach (var item in modifiedEntities)
            {
                IBaseEntity entity = item.Entity as IBaseEntity;

                if (item != null)
                {
                    switch (item.State)
                    {
                        case EntityState.Added:
                            entity.CreatedDate = date;
                            entity.CreatedIP = ipAddress;
                            entity.CreatedComputerName = computerName;
                            break;
                        case EntityState.Modified:
                            entity.UpdatedDate = date;
                            entity.UpdatedIP = ipAddress;
                            entity.UpdatedComputerName = computerName;
                            break;
                        case EntityState.Deleted:
                            entity.DeletedIP = ipAddress;
                            entity.DeletedDate= date;
                            entity.DeletedComputerName= computerName;
                            break;
                    }
                }
            }

            return base.SaveChanges();
        }
    }
}
