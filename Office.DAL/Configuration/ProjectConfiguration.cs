using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Office.DAL.Entity;

namespace Office.DAL.Configuration;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasMany(i => i.Employees)
            .WithMany(i => i.Projects);

        builder.HasOne(i => i.ProjectType)
            .WithMany(i => i.Projects)
            .OnDelete(DeleteBehavior.Restrict);
    }
}