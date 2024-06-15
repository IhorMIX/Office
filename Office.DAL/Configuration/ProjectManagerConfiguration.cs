using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Office.DAL.Entity;

namespace Office.DAL.Configuration
{
    public class ProjectManagerConfiguration : IEntityTypeConfiguration<ProjectManager>
    {
        public void Configure(EntityTypeBuilder<ProjectManager> builder)
        {
            // Указываем, что Employee наследует от BaseUser
            builder.HasBaseType<BaseUser>();
            
            
        }
    }
}