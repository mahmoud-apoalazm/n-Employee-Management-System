using Entities.Constants;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Configuration
{
    public class ManagerConfiguration : IEntityTypeConfiguration<Manager>
    {
        public void Configure(EntityTypeBuilder<Manager> builder)
        {
            builder
            .Property(e => e.Id)
            .HasColumnName("ManagerId");

            builder.Property(e => e.Address).IsRequired();
            builder.Property(e => e.DateOfBirth).IsRequired().HasColumnType("date"); ;
            builder.Property(e => e.DateOfJoin).IsRequired();
            builder.Property(e => e.Position).IsRequired();
            builder.Property(e => e.Salary).IsRequired();
            builder.Property(e => e.Gender).IsRequired();
            builder
            .HasOne(e => e.Department)
            .WithMany(e => e.Managers)
            .OnDelete(DeleteBehavior.SetNull);

            var converter = new ValueConverter<Gender, string>(
       v => v.ToString(),
       v => (Gender)Enum.Parse(typeof(Gender), v));

            builder
           .Property(e => e.Gender)
           .HasConversion(converter);

            //-----------------------------------------------------------------------------------------
            builder.HasData
      (
      new Manager
      {
          Id = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"),
          Name = "Hussein El Shahat",
          Address = "Cairo",
          Gender = Gender.Male,
          DateOfBirth = new DateTime(2000, 5, 1),
          DateOfJoin = DateTime.Now,
          Position = "Manager",
          Salary = 9000m,
          DepartmentId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870")
      },
      new Manager
      {
          Id = new Guid("0a0ca5a3-9c97-4bd4-82c5-2994b1ba1637"),
          Name = "Mohamed Apo Treka",
          Address = "Tanta",
          Gender = Gender.Male,
          DateOfBirth = new DateTime(2000, 5, 1),
          DateOfJoin = DateTime.Now,
          Position = "Manager",
          Salary=9000m,
          DepartmentId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870")
      },
      new Manager
      {
          Id = new Guid("f0e10c08-873e-41a3-b545-0466a2a8b1b2"),
          Name = "Mohamed Awad",
          Address = "Cairo",
          Gender = Gender.Male,
          DateOfBirth = new DateTime(2000, 5, 1),
          DateOfJoin = DateTime.Now,
          Position = "Manager",
          Salary = 9000m,
          DepartmentId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3")
      }
      );
        }
    }
}
