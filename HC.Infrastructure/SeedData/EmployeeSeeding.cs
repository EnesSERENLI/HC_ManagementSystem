using HC.Domain.Entities.Concrete;
using HC.Infrastructure.Context;
using HC.Infrastructure.Repositories.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Infrastructure.SeedData
{
    public class EmployeeSeeding : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasData(
                new Employee { ID = Guid.NewGuid(), FirstName = "Enes", LastName = "Serenli", Title = "Mr."},
                new Employee { ID = Guid.NewGuid(), FirstName = "Fatih", LastName = "Serenli", Title = "Mr."},
                new Employee { ID = Guid.NewGuid(), FirstName = "Semih", LastName = "Serenli", Title = "Mr."},
                new Employee { ID = Guid.NewGuid(), FirstName = "Onur", LastName = "Doğru", Title = "Mr."}
                );
        }
    }
}
