using HC.Domain.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Infrastructure.SeedData
{
    public class CategorySeeding : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                new Category {ID=Guid.NewGuid(),CategoryName = "Korea",Description = "Excellent korean food" },
                new Category {ID=Guid.NewGuid(),CategoryName = "Japan",Description = "Excellent Japan food" },
                new Category {ID=Guid.NewGuid(),CategoryName = "China",Description = "Excellent China food" }
                );
        }
    }
}
