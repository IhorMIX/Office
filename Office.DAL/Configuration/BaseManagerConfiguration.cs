using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Office.DAL.Entity;
using Office.DAL.Entity.Employees;

namespace Office.DAL.Configuration;

public class BaseManagerConfiguration : IEntityTypeConfiguration<BaseManager>
{
    public void Configure(EntityTypeBuilder<BaseManager> builder)
    {
        builder.HasMany(i => i.ApprovalRequests)
            .WithOne(i => i.Approver)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(i => i.AuthorizationInfo)
            .WithOne(i => (BaseManager)i.Employee)
            .HasForeignKey<AuthorizationInfo>(i => i.EmployeeId);
    }
}