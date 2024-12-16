using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Office.DAL.Entity;

namespace Office.DAL.Configuration;

public class LeaveRequestConfiguration : IEntityTypeConfiguration<LeaveRequest>
{
    public void Configure(EntityTypeBuilder<LeaveRequest> builder)
    {
        builder.HasKey(lr => lr.Id);
        
        builder.HasOne(i => i.Employee)
            .WithMany(i => i.LeaveRequests)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(i => i.AbsenceReason)
            .WithMany(i => i.LeaveRequests)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(lr => lr.ApprovalRequest)
            .WithOne(ar => ar.LeaveRequest)
            .OnDelete(DeleteBehavior.Cascade);
    }
}