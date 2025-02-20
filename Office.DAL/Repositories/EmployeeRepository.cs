using Microsoft.EntityFrameworkCore;
using Office.DAL.Entity;
using Office.DAL.Entity.Employees;
using Office.DAL.Repositories.Intefaces;

namespace Office.DAL.Repositories;

public class EmployeeRepository(OfficeDbContext officeDbContext) : IEmployeeRepository
{
    private readonly OfficeDbContext _officeDbContext = officeDbContext;

    public IQueryable<BaseEmployee> GetAll()
    {
        return _officeDbContext.BaseEmployees
            .Include(i=>((Employee)i).Position)
            .Include(r => ((Employee)r).Subdivision)
            .AsQueryable();
    }

    public IQueryable<BaseManager> GetAllManagers()
    {
        return _officeDbContext.Managers.Include(i => i.ApprovalRequests)
            .AsQueryable();
    }
    
    public IQueryable<Employee> GetAllEmployees()
    {
        return _officeDbContext.Employees
            .Include(r => r.Position)
            .Include(r => r.Subdivision)
            .Include(r => r.HrManager)
            .AsQueryable();
    }
    public IQueryable<HrManager> GetAllHrManagers()
    {
        return _officeDbContext.HrManagers
            .Include(i => i.Workers)
            .AsQueryable();
    }
    public IQueryable<ProjectManager> GetAllProjectManagers()
    {
        return _officeDbContext.ProjectManagers
            .Include(r => r.Projects)
            .AsQueryable();
    }
    
    public async Task<BaseEmployee?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _officeDbContext.BaseEmployees
            .Include(r => ((Employee)r).Subdivision)
            .Include(r => ((Employee)r).Position)
            .Include(r => ((Employee)r).Projects)
            .Include(r => ((Employee)r).LeaveRequests)
            .Include(r => ((Employee)r).HrManager)
            .Include(r => ((HrManager)r).Workers)
            .Include(r => ((ProjectManager)r).Projects).ThenInclude(r => r.ProjectType)
            .SingleOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task<BaseEmployee> AddEmployeeAsync(BaseEmployee employee, CancellationToken cancellationToken = default)
    {
        var employeeDb = await _officeDbContext.BaseEmployees.AddAsync(employee,cancellationToken);
        await _officeDbContext.SaveChangesAsync(cancellationToken);
        return employeeDb.Entity;
    }

    public async Task DeleteEmployeeAsync(BaseEmployee employee, CancellationToken cancellationToken = default)
    {
        _officeDbContext.BaseEmployees.Remove(employee);
        await _officeDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<BaseEmployee> UpdateEmployeeAsync(BaseEmployee employee, CancellationToken cancellationToken = default)
    {
        var employeeDb = _officeDbContext.BaseEmployees.Update(employee);
        await _officeDbContext.SaveChangesAsync(cancellationToken);
        return employeeDb.Entity;
    }
}