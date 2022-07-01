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
    public class SubCategoryMap:BaseMap<SubCategory>
    {
        public override void Configure(EntityTypeBuilder<SubCategory> builder)
        {
            builder.HasKey(x=>x.ID);

            builder.Property(x => x.SubCategoryName).IsRequired(true).HasMaxLength(255);
            builder.Property(x => x.Description).IsRequired(true).HasMaxLength(1000);

            builder.HasOne(x => x.Category)
                .WithMany(x => x.SubCategories)
                .HasForeignKey(x => x.CategoryId);

            base.Configure(builder);
        }
    }
}
