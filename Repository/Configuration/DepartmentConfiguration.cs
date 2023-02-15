using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Configuration
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder
             .Property(e => e.Id)
             .HasColumnName("DepartmentId");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(60);

            builder.Property(e => e.Location)
                .IsRequired()
                .HasMaxLength(60);

            //----------------------------------------------------------------
            builder.HasData
            (
            new Department
            {
                Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                Name = "Quality Department",
                Location="Tanta"
            },
            new Department
            {
                Id = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                Name = "Finance Department",
                Location = "Tanta"
            }
            );

        }
    }
}
