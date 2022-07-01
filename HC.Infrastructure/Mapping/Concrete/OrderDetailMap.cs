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
    public class OrderDetailMap:BaseMap<OrderDetail>
    {
        public override void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasKey(x => x.ID);

            builder.Property(x=>x.UnitPrice)
                .HasPrecision(18,2)
                .HasConversion<decimal>()
                .IsRequired(true);

            builder.Property(x=>x.Quantity).IsRequired(true);

            builder.HasOne(x => x.Product)
                .WithMany(x => x.OrderDetails)
                .HasForeignKey(x => x.ProductId);

            builder.HasOne(x => x.Order)
                .WithMany(x => x.OrderDetails)
                .HasForeignKey(x => x.OrderId);

            base.Configure(builder);
        }
    }
}
