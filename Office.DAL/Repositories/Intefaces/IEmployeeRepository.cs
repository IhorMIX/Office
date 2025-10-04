using Office.DAL.Entity;
using Office.DAL.Entity.Employees;

namespace Office.DAL.Repositories.Intefaces;

public interface IEmployeeRepository : IBasicRepository<BaseEmployee>
{
    Task<BaseEmployee> AddEmployeeAsync(BaseEmployee employee, CancellationToken cancellationToken = default);
    Task DeleteEmployeeAsync(BaseEmployee employee, CancellationToken cancellationToken = default);
    Task<BaseEmployee> UpdateEmployeeAsync(BaseEmployee employee, CancellationToken cancellationToken = default);
    Task<BaseEmployee?> GetManagerByIdAsync(int id, CancellationToken cancellationToken = default);
    IQueryable<BaseManager> GetAllManagers();
    IQueryable<Employee> GetAllEmployees();
    IQueryable<HrManager> GetAllHrManagers();
    IQueryable<ProjectManager> GetAllProjectManagers();
    IQueryable<BaseManager> GetAdmin();
    Task<List<BaseEmployee>> GetByIdsAsync(List<int> ids, CancellationToken cancellationToken = default);
}