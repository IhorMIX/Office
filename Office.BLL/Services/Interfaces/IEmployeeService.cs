using Office.BLL.Models;

namespace Office.BLL.Services.Interfaces;

public interface IEmployeeService : IBasicService<EmployeeModel>
{
    Task<EmployeeModel> CreateEmployeeAsync(int managerId, EmployeeModel employeeModel, CancellationToken cancellationToken = default);
    Task<EmployeeModel> UpdateEmployeeAsync(int managerId, EmployeeModel employeeModel, CancellationToken cancellationToken = default);
    Task DeleteEmployeeAsync(int id, CancellationToken cancellationToken = default);
    
    Task<List<EmployeeModel>> GetEmployeesAsync(int managerId, CancellationToken cancellationToken = default);
    Task<List<EmployeeModel>> GetAllAsync(int managerId, CancellationToken cancellationToken = default);
}