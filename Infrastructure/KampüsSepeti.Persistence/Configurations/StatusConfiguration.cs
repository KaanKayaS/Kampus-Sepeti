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
    public class StatusConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            Status status1 = new()
            {
                Id = 1,
                Name = "Çok iyi",
                CreatedDate = DateTime.Now,
                IsDeleted = false,
            };

            Status status2 = new()
            {
                Id = 2,
                Name = "Kötü",
                CreatedDate = DateTime.Now,
                IsDeleted = false,
            };

            Status status3 = new()
            {
                Id = 3,
                Name = "İdare eder",
                CreatedDate = DateTime.Now,
                IsDeleted = false,
            };

            Status status4 = new()
            {
                Id = 4,
                Name = "Az kullanılmış",
                CreatedDate = DateTime.Now,
                IsDeleted = false,
            };

            builder.HasData(status1, status2, status3, status4);

        }
    }
}
