using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Office.DAL.Entity;

namespace Office.DAL.Configuration;

public class TaskEntityConfiguration : IEntityTypeConfiguration<TaskEntity>
{
    public void Configure(EntityTypeBuilder<TaskEntity> builder)
    {
        builder.HasOne(t => t.Employee)
            .WithMany(e => e.Tasks)
            .HasForeignKey(t => t.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);
            
        builder.HasOne(t => t.Project)
            .WithMany(p => p.Tasks)
            .HasForeignKey(t => t.ProjectId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}