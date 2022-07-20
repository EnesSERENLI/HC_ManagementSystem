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
    public class ProductSeeding : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                new Product { ID = Guid.NewGuid(), ProductName = "Tteokbokki Ramen", Description = "Spicy rice cakes, serving with fish cake.", UnitPrice = 10.99m, UnitsInStock = 250/*, SubCategoryId = _db.SubCategories.FirstOrDefault(x => x.SubCategoryName == "Korea").ID */},
                new Product { ID = Guid.NewGuid(), ProductName = "Bibimbap Ramen", Description = "Boiling vegetables, serving with special hot sauce", UnitPrice = 8.99m, UnitsInStock = 300/*, SubCategoryId = _db.SubCategories.FirstOrDefault(x => x.SubCategoryName == "Korea").ID */},
                new Product { ID = Guid.NewGuid(), ProductName = "Jajangmyeon Ramen", Description = "Black bean sauce noodle, serving with green onion", UnitPrice = 15.99m, UnitsInStock = 500/*, SubCategoryId = _db.SubCategories.FirstOrDefault(x => x.SubCategoryName == "Korea").ID */},
                new Product { ID = Guid.NewGuid(), ProductName = "Chicken Ramen", Description = "Chicken noodle soup, serving with vegetables such as soy bean, green onion. In an optional you can ask for egg.", UnitPrice = 7.99m, UnitsInStock = 150/*, SubCategoryId = _db.SubCategories.FirstOrDefault(x => x.SubCategoryName == "Japan").ID */},
                new Product { ID = Guid.NewGuid(), ProductName = "Onigiri Ramen", Description = "Rice Sandwich, serving with soy sauce", UnitPrice = 9.99m, UnitsInStock = 200/*, SubCategoryId = _db.SubCategories.FirstOrDefault(x => x.SubCategoryName == "Japan").ID */},
                new Product { ID = Guid.NewGuid(), ProductName = "Doroyaki Ramen", Description = "Red bean paste dessert, serving with honey.", UnitPrice = 3.99m, UnitsInStock = 450/*, SubCategoryId = _db.SubCategories.FirstOrDefault(x => x.SubCategoryName == "Japan").ID */},
                new Product { ID = Guid.NewGuid(), ProductName = "Dan Dan Mian Ramen", Description = "Dan dan noodle, serving with green onion", UnitPrice = 5.99m, UnitsInStock = 300/*, SubCategoryId = _db.SubCategories.FirstOrDefault(x => x.SubCategoryName == "China").ID */},
                new Product { ID = Guid.NewGuid(), ProductName = "Yangzhou Fried Rice Ramen", Description = "Yangzhou style fried rice, serving with bean and pickles", UnitPrice = 12.99m, UnitsInStock = 150/*, SubCategoryId = _db.SubCategories.FirstOrDefault(x => x.SubCategoryName == "China").ID */},
                new Product { ID = Guid.NewGuid(), ProductName = "Ma Yi Shang Shu Ramen", Description = "Hot pepper sauce noodle, serving with soy bean and onion", UnitPrice = 14.99m, UnitsInStock = 170/*, SubCategoryId = _db.SubCategories.FirstOrDefault(x => x.SubCategoryName == "China").ID */}
                );
        }
    }
}
