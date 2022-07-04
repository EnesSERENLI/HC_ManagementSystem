using HC.Domain.Entities.Concrete;
using HC.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Infrastructure.SeedData
{
    public class DepartmentSeeding : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasData(
                new Department { ID = Guid.NewGuid(), DepartmentName = "General management" },
                new Department { ID = Guid.NewGuid(), DepartmentName = "Purchasing" },
                new Department { ID = Guid.NewGuid(), DepartmentName = "Waiter" },
                new Department { ID = Guid.NewGuid(), DepartmentName = "Sales" }
                );
        }
    }
}
