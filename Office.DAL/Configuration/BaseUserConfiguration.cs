using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Office.DAL.Entity;
using Office.DAL.Entity.Enums;

namespace Office.DAL.Configuration;

public class BaseUserConfiguration : IEntityTypeConfiguration<BaseUser>
{
    public void Configure(EntityTypeBuilder<BaseUser> builder)
    {
        builder.HasKey(bu => bu.Id);
    }
}