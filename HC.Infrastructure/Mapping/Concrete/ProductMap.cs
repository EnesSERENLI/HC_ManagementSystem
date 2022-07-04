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
    public class ProductMap:BaseMap<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x=>x.ID);
            builder.Property(x=>x.ProductName).IsRequired(true).HasMaxLength(255);
            builder.Property(x=>x.Description).IsRequired(false).HasMaxLength(1000);
            builder.Property(x => x.UnitPrice).IsRequired(true)
                .HasPrecision(18, 2)
                .HasConversion(typeof(decimal));
            builder.Property(x=>x.UnitsInStock).IsRequired(true)
                .HasConversion<short>();

            builder.Property(x => x.ImagePath).IsRequired(false).HasMaxLength(500);

            builder.HasOne(x => x.SubCategory)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.SubCategoryId);
            
            base.Configure(builder);
        }
    }
}
