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
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(20);

            Location location1 = new()
            {
                Id = 1,
                Name = "Antalya",
                CreatedDate = DateTime.Now,
                IsDeleted = false,
            };

            Location location2 = new()
            {
                Id = 2,
                Name = "Bursa",
                CreatedDate = DateTime.Now,
                IsDeleted = false,
            };

            Location location3 = new()
            {
                Id = 3,
                Name = "İstanbul",
                CreatedDate = DateTime.Now,
                IsDeleted = false,
            };

            Location location4 = new()
            {
                Id = 4,
                Name = "Ankara",
                CreatedDate = DateTime.Now,
                IsDeleted = false,
            };

            Location location5 = new()
            {
                Id = 5,
                Name = "Kütahya",
                CreatedDate = DateTime.Now,
                IsDeleted = false,
            };

            builder.HasData(location1, location2, location3, location4, location5);

        }
    }
}
