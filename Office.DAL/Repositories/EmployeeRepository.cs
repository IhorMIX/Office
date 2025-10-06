using Microsoft.EntityFrameworkCore;
using Office.DAL.Entity;
using Office.DAL.Entity.Employees;
using Office.DAL.Repositories.Intefaces;

namespace Office.DAL.Repositories;

public class EmployeeRepository(OfficeDbContext officeDbContext) : IEmployeeRepository
{
    public IQueryable<BaseEmployee> GetAll()
    {
        return officeDbContext.BaseEmployees
            .Include(i=>((Employee)i).Position)
            .Include(r => ((Employee)r).Subdivision)
            .AsQueryable();
    }
    public IQueryable<BaseManager> GetAdmin()
    {
        return officeDbContext.Admins.AsNoTracking();
    }
    
    public IQueryable<BaseManager> GetAllManagers()
    {
        return officeDbContext.Managers.Include(i => i.ApprovalRequests)
            .AsNoTracking();
    }
    
    public IQueryable<Employee> GetAllEmployees()
    {
        return officeDbContext.Employees
            .Include(r => r.Position)
            .Include(r => r.Subdivision)
            .Include(r => r.HrManager)
            .AsQueryable();
    }

    public IQueryable<HrManager> GetAllHrManagers()
    {
        return officeDbContext.HrManagers
            .Include(i => i.Workers)
            .AsNoTracking();
    }
    public IQueryable<ProjectManager> GetAllProjectManagers()
    {
        return officeDbContext.ProjectManagers
            .Include(r => r.Projects)
            .ThenInclude(r=>r.ProjectType)
            .AsNoTracking();
    }
    public async Task<BaseEmployee?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return  await officeDbContext.BaseEmployees
            .Include(r => (r as Employee)!.Subdivision)
            .Include(r => (r as Employee)!.Position)
            .Include(r => (r as Employee)!.Projects)
            .Include(r => (r as Employee)!.LeaveRequests)
            .Include(r => (r as Employee)!.HrManager)
            .Include(r => (r as HrManager)!.Workers)
            .Include(r => (r as ProjectManager)!.Projects)
            .ThenInclude(p => p.ProjectType)
            .SingleOrDefaultAsync(r => r.Id == id, cancellationToken);
    }
    
    public Task<List<BaseEmployee>> GetByIdsAsync(List<int> ids, CancellationToken cancellationToken = default)
    {
        return officeDbContext.BaseEmployees.Where(u => ids.Contains(u.Id)).ToListAsync(cancellationToken);
    }
    public async Task<BaseEmployee?> GetManagerByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await officeDbContext.BaseEmployees
            .AsNoTracking()
            .Where(e => e is HrManager || e is ProjectManager)
            .Include(r => (r as HrManager)!.Workers)
            .ThenInclude(w => w.Position)
            .Include(r => (r as ProjectManager)!.Projects)
            .ThenInclude(p => p.ProjectType)
            .SingleOrDefaultAsync(r => r.Id == id, cancellationToken);
    }


    public async Task<BaseEmployee> AddEmployeeAsync(BaseEmployee employee, CancellationToken cancellationToken = default)
    {
        var employeeDb = await officeDbContext.BaseEmployees.AddAsync(employee,cancellationToken);
        await officeDbContext.SaveChangesAsync(cancellationToken);
        return employeeDb.Entity;
    }

    public async Task DeleteEmployeeAsync(BaseEmployee employee, CancellationToken cancellationToken = default)
    {
        officeDbContext.BaseEmployees.Remove(employee);
        await officeDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<BaseEmployee> UpdateEmployeeAsync(BaseEmployee employee, CancellationToken cancellationToken = default)
    {
        var employeeDb = officeDbContext.BaseEmployees.Update(employee);
        await officeDbContext.SaveChangesAsync(cancellationToken);
        return employeeDb.Entity;
    }
}