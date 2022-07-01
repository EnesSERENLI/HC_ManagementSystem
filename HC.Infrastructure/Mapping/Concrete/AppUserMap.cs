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
    public class AppUserMap:BaseMap<AppUser>
    {
        public override void Configure(EntityTypeBuilder<AppUser> builder)
        {

            builder.Property(x => x.FullName).IsRequired(true).HasMaxLength(100);
            builder.Property(x => x.ImagePath).IsRequired(false).HasMaxLength(500);

            builder.Property(x => x.UserName).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.Email).IsRequired(true).HasMaxLength(255);
            builder.Property(x => x.BirthDate).IsRequired(false);
            builder.Property(x => x.Address).IsRequired(false).HasMaxLength(500);


            base.Configure(builder);
        }
    }
}
