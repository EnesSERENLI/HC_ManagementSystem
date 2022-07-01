using HC.Domain.Entities.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Infrastructure.Mapping.Abstract
{
    public abstract class BaseMap<T> : IEntityTypeConfiguration<T> where T : class, IBaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.Status).IsRequired(true);

            builder.Property(x => x.CreatedIP).IsRequired(false).HasMaxLength(255);
            builder.Property(x => x.CreatedDate).IsRequired(false);
            builder.Property(x => x.CreatedComputerName).IsRequired(false).HasMaxLength(255);
            builder.Property(x => x.CreatorUserName).IsRequired(false).HasMaxLength(255);

            builder.Property(x=> x.UpdatedIP).IsRequired(false).HasMaxLength(255);
            builder.Property(x=> x.UpdatedDate).IsRequired(false);
            builder.Property(x=> x.UpdaterUserName).IsRequired(false).HasMaxLength(255);
            builder.Property(x=> x.UpdatedComputerName).IsRequired(false).HasMaxLength(255);

            builder.Property(x => x.DeletedIP).IsRequired(false).HasMaxLength(255);
            builder.Property(x => x.DeletedDate).IsRequired(false);
            builder.Property(x => x.DeleterUserName).IsRequired(false).HasMaxLength(255);
            builder.Property(x => x.DeletedComputerName).IsRequired(false).HasMaxLength(255);

        }
    }
}
