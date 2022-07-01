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
    public class DepartmentMap:BaseMap<Department>
    {
        public override void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(x=>x.ID);

            builder.Property(x=>x.DepartmentName).IsRequired(true).HasMaxLength(100);

            base.Configure(builder);
        }
    }
}
