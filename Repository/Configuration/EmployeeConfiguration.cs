using Entities.Constants;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Numerics;
using System.Reflection.Emit;

namespace Repository.Configuration;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder
            .Property(e => e.Id)
            .HasColumnName("EmployeeId");
        builder.Property(e => e.Address).IsRequired();
        builder.Property(e => e.DateOfBirth).IsRequired().HasColumnType("date"); ;
        builder.Property(e => e.DateOfJoin).IsRequired();
        builder.Property(e => e.Position).IsRequired();
        builder.Property(e => e.Salary).IsRequired();
        builder.Property(e => e.Gender).IsRequired();
        builder
           .HasOne(e => e.Department)
           .WithMany(e => e.Employees)
           .OnDelete(DeleteBehavior.SetNull);

        builder
           .HasOne(e => e.Manager)
           .WithMany(e => e.Employees)
           .HasForeignKey(c => c.ManagerId)
            .OnDelete(DeleteBehavior.SetNull);

        var converter = new ValueConverter<Gender, string>(
        v => v.ToString(),
        v => (Gender)Enum.Parse(typeof(Gender), v));

             builder
            .Property(e => e.Gender)
            .HasConversion(converter);



        builder.HasMany(e => e.Projects)
            .WithMany(p => p.Employees)
            .UsingEntity<EmployeeProject>(
            j => j
            .HasOne(ep => ep.Project)
            .WithMany(e => e.EmployeeProjects)
            .HasForeignKey(ep => ep.ProjectId),
            j => j
            .HasOne(ep => ep.Employee)
            .WithMany(e => e.EmployeeProjects)
            .HasForeignKey(ep => ep.EmployeeId),
            j => j.HasKey(t => new { t.EmployeeId, t.ProjectId })
            );

        //-------------------------------------------------------------------------
        builder.HasData
       (
       new Employee
       {
           Id = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"),
           Name = "Hussein El Shahat",
           Address = "Cairo",
           Gender = Gender.Male,
           DateOfBirth= new DateTime(2000, 5, 1),
           DateOfJoin=DateTime.Now,
           Position="Developer",
           Salary = 9000m,
           ManagerId = new Guid("0a0ca5a3-9c97-4bd4-82c5-2994b1ba1637"),
           DepartmentId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870")
       },
       new Employee
       {
           Id = new Guid("86dba8c0-d178-41e7-938c-ed49778fb51a"),
           Name = "Mohamed Apo Treka",
           Address="Tanta",
           Gender=Gender.Male,
           DateOfBirth = new DateTime(2000, 5, 1),
           DateOfJoin = DateTime.Now,
           Position = "Developer",
           Salary = 9000m,
           ManagerId = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"),

           DepartmentId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870")
       },
       new Employee
       {
           Id = new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"),
           Name = "Mohamed Awad",
           Address = "Cairo",
           Gender = Gender.Male,
           DateOfBirth = new DateTime(2000, 5, 1),
           DateOfJoin = DateTime.Now,
           Position = "Developer",
           Salary = 9000m,
           ManagerId = new Guid("f0e10c08-873e-41a3-b545-0466a2a8b1b2"),
           DepartmentId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3")
       }
       );

    }
}
