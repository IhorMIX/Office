using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Office.DAL.Entity;

namespace Office.DAL.Configuration;

public class ApprovalRequestConfiguration : IEntityTypeConfiguration<ApprovalRequest>
{
    public void Configure(EntityTypeBuilder<ApprovalRequest> builder)
    {
        builder.HasOne(i => i.LeaveRequest)
            .WithOne(i => i.ApprovalRequest)
            .HasForeignKey<ApprovalRequest>(ar => ar.LeaveRequestId);

        builder.HasOne(i => i.Approver)
            .WithMany()
            .HasForeignKey(i => i.ApproverId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}