using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Office.DAL.Entity;

namespace Office.DAL.Configuration;

public class HrManagerConfiguration : IEntityTypeConfiguration<HrManager>
{
    public void Configure(EntityTypeBuilder<HrManager> builder)
    {
        builder.HasMany(i => i.Workers)
            .WithOne(i => i.HrManager)
            .HasForeignKey(i => i.HrManagerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}