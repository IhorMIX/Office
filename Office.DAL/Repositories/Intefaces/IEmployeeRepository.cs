using Office.DAL.Entity;
using Office.DAL.Entity.Employees;

namespace Office.DAL.Repositories.Intefaces;

public interface IEmployeeRepository : IBasicRepository<BaseEmployee>
{
    Task<BaseEmployee> AddEmployeeAsync(BaseEmployee employee, CancellationToken cancellationToken = default);
    Task DeleteEmployeeAsync(BaseEmployee employee, CancellationToken cancellationToken = default);
    Task<BaseEmployee> UpdateEmployeeAsync(BaseEmployee employee, CancellationToken cancellationToken = default);
    
    IQueryable<BaseManager> GetAllManagers();
    IQueryable<Employee> GetAllEmployees();
    IQueryable<HrManager> GetAllHrManagers();
    IQueryable<ProjectManager> GetAllProjectManagers();
}