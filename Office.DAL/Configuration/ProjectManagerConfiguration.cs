using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Office.DAL.Entity;

namespace Office.DAL.Configuration;

public class ProjectManagerConfiguration : IEntityTypeConfiguration<ProjectManager>
{
    public void Configure(EntityTypeBuilder<ProjectManager> builder)
    {
        builder.HasMany(i => i.Projects)
            .WithOne(i => i.ProjectManager)
            .HasForeignKey(i => i.ProjectManagerId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}