using Microsoft.EntityFrameworkCore;
using Office.DAL.Entity;
using Office.DAL.Entity.Enums;

namespace Office.DAL;

public class OfficeDbContext : DbContext
{
    public OfficeDbContext(DbContextOptions<OfficeDbContext> options) : base(options)
    {
    }
    public DbSet<BaseUser> Users { get; set; }
    //public DbSet<LeaveRequest> LeaveRequests { get; set; }
    //public DbSet<ApprovalRequest> ApprovalRequests { get; set; }
    //public DbSet<Project> Projects { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OfficeDbContext).Assembly);
    }
}