using Microsoft.EntityFrameworkCore;
using Office.DAL.Entity;

namespace Office.DAL;

public class OfficeDbContext : DbContext
{
    public OfficeDbContext(DbContextOptions<OfficeDbContext> options) : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
}