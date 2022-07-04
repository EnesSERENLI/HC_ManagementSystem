using HC.Domain.Entities.Concrete;
using HC.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Infrastructure.SeedData
{
    public class SubCategorySeeding : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {
            builder.HasData(
                new SubCategory { ID = Guid.NewGuid(), SubCategoryName = "Korean Ramen", Description = "Excellent Korean Ramen"/*, CategoryId = _db.Categories.FirstOrDefault(x => x.CategoryName == "Korea").ID */},
                new SubCategory { ID = Guid.NewGuid(), SubCategoryName = "Japan Ramen", Description = "Excellent Japan Ramen"/*, CategoryId = _db.Categories.FirstOrDefault(x => x.CategoryName == "Japan").ID */},
                new SubCategory { ID = Guid.NewGuid(), SubCategoryName = "China Ramen", Description = "Excellent China Ramen"/*, CategoryId = _db.Categories.FirstOrDefault(x => x.CategoryName == "China").ID */}
                );
        }
    }
}
