using KampüsSepeti.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Persistence.Configurations
{
    public class CategoriesConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            Category category1 = new()
            {
                Id = 1,
                Name = "A",
                CreatedDate = DateTime.Now,
                IsDeleted = false,
            };

            Category category2 = new()
            {
                Id = 2,
                Name = "B",
                CreatedDate = DateTime.Now,
                IsDeleted = false,
            };

            Category category3 = new()
            {
                Id = 3,
                Name = "C",
                CreatedDate = DateTime.Now,
                IsDeleted = false,
            };

            Category category4 = new()
            {
                Id = 4,
                Name = "D",
                CreatedDate = DateTime.Now,
                IsDeleted = false,
            };

            builder.HasData(category1,category2, category3, category4);
        }
    }
}
