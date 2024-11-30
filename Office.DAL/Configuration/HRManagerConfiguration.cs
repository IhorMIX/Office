using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Office.DAL.Entity;

namespace Office.DAL.Configuration
{
    public class HRManagerConfiguration : IEntityTypeConfiguration<HRManager>
    {
        public void Configure(EntityTypeBuilder<HRManager> builder)
        {
            // Указываем, что Employee наследует от BaseUser
            builder.HasBaseType<BaseEmployee>();
            
            
        }
    }
}