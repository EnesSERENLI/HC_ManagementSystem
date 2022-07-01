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
    public class CategoryMap:BaseMap<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x=>x.ID);

            builder.Property(x => x.CategoryName).IsRequired(true)
                .HasMaxLength(255);
            builder.Property(x=>x.Description).IsRequired(true).HasMaxLength(1000);

            base.Configure(builder);
        }
    }
}
