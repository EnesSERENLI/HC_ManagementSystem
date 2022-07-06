using HC.Domain.Entities.Concrete;
using HC.Infrastructure.Mapping.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Infrastructure.Mapping.Concrete
{
    public class EmployeeMap:BaseMap<Employee>
    {
        public override void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(x=>x.ID);

            builder.Property(x=>x.FirstName).IsRequired(true).HasMaxLength(50);
            builder.Property(x=>x.LastName).IsRequired(true).HasMaxLength(100);
            builder.Property(x=>x.Title).IsRequired(true).HasMaxLength(50);
            builder.Property(x=>x.Address).IsRequired(false).HasMaxLength(500);

            builder.HasOne(x => x.Department)
                .WithMany(x => x.Employees)
                .HasForeignKey(x => x.DepartmentId);

            base.Configure(builder);
        }
    }
}
