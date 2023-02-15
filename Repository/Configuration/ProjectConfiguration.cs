using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Repository.Configuration
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder
            .Property(e => e.Id)
            .HasColumnName("ProjectId");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(60);
            builder.Property(e => e.Description)
                   .HasMaxLength(200);
            //------------------------------------------------
            builder.HasData
           (
           new Project
           {
               Id = new Guid("6b676cb8-2fa0-4b77-a33d-c94cf69253d0"),
               Name= "Task Payment",
               
           },
           new Project
           {
               Id = new Guid("b46df07b-e650-4a70-9475-b85f84274855"),
               Name = "Task CheckOut",
           }
           );


            //---------------------------------------------------------------
   
        }
      
    }
}
