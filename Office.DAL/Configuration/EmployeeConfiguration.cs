using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Office.DAL.Entity;

namespace Office.DAL.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasBaseType<BaseUser>();
            
            builder.HasOne<HRManager>()
                .WithMany()
                .HasForeignKey(e => e.PeoplePartnerID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}