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
    public class OrderMap:BaseMap<Order>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.ID);
            //builder.Property(x=>x.OrderID).IsRequired(false);

            builder.HasOne(x => x.Employee)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.EmployeeId);

            //builder.HasOne(x=>x.AppUser)
            //    .WithMany(x=>x.Orders)
            //    .HasForeignKey(x=>x.AppUserId);

            base.Configure(builder);
        }
    }
}
