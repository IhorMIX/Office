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
    
    public Task<BaseEmployee?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return _officeDbContext.BaseEmployees.SingleOrDefaultAsync(i => i.Id == id, cancellationToken: cancellationToken);
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