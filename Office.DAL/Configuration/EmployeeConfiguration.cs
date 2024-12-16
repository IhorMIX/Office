using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Office.DAL.Entity;
using Office.DAL.Entity.Employees;

namespace Office.DAL.Configuration;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasOne(r => r.Subdivision)
            .WithMany(r => r.Employees)
            .HasForeignKey(r => r.SubdivisionId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(r => r.Position)
            .WithMany(r => r.Employees)
            .HasForeignKey(r => r.PositionId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(r => r.HrManager)
            .WithMany(r => r.Workers)
            .HasForeignKey(r => r.HrManagerId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(i => i.AuthorizationInfo)
            .WithOne(i => (Employee)i.Employee)
            .HasForeignKey<AuthorizationInfo>(i => i.EmployeeId);
    }
}