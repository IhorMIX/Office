using Microsoft.EntityFrameworkCore;
using Office.DAL.Entity;
using Office.DAL.Entity.Employees;
using Office.DAL.Entity.Selections;

namespace Office.DAL;

public class OfficeDbContext(DbContextOptions<OfficeDbContext> options) : DbContext(options)
{
    public DbSet<BaseEmployee> BaseEmployees { get; set; }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<HrManager> HrManagers { get; set; }
    public DbSet<ProjectManager> ProjectManagers { get; set; }

    public DbSet<LeaveRequest> LeaveRequests { get; set; }
    public DbSet<ApprovalRequest> ApprovalRequests { get; set; }

    public DbSet<AbsenceReason> AbsenceReasons { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<ProjectType> ProjectTypes { get; set; }
    public DbSet<Subdivision> Subdivisions { get; set; }

    public DbSet<Project> Projects { get; set; }
    public DbSet<BaseManager> Managers { get; set; }

    public DbSet<AuthorizationInfo> AuthorizationInfos { get; set; }
    public DbSet<Admin> Admins { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>().HasData(
            new Admin
            {
                Id = 1,
                Login = "admin",
                Password = "ANQE9vuNoOS6J1Af8yk/a4nBg++OJRyeEc87sqXiqDwKdFgXGKGZqmbw104u/9R4EA==",
                FullName = "ADMIN",
                isDeactivated = false,
            });
        
        modelBuilder.Entity<Position>().HasData(
            new Position { Id = 1, Name = "Backend Developer" },
            new Position { Id = 2, Name = "Frontend Developer" },
            new Position { Id = 3, Name = "QA Engineer" },
            new Position { Id = 4, Name = "UI/UX Designer" }
        );

        modelBuilder.Entity<Subdivision>().HasData(
            new Subdivision { Id = 1, Name = "Development" },
            new Subdivision { Id = 2, Name = "Data" },
            new Subdivision { Id = 3, Name = "Support" },
            new Subdivision { Id = 4, Name = "Security" },
            new Subdivision { Id = 5, Name = "QA" }
        );

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OfficeDbContext).Assembly);
    }
}