using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Office.BLL.Exceptions;
using Office.BLL.Models;
using Office.BLL.Services.Interfaces;
using Office.DAL.Repositories.Intefaces;


namespace Office.BLL.Services;

public class EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper) : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<EmployeeModel> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var employeeDb = await _employeeRepository.GetAllEmployees()
            .SingleOrDefaultAsync(i => i.Id == id, cancellationToken);
        
        if (employeeDb is null)
            throw new EmployeeNotFoundException($"Employee with Id {id} not found");

        return _mapper.Map<EmployeeModel>(employeeDb);
    }

    public Task<EmployeeModel> CreateEmployeeAsync(int managerId, EmployeeModel employeeModel, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<EmployeeModel> UpdateEmployeeAsync(int managerId, EmployeeModel employeeModel, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteEmployeeAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<EmployeeModel>> GetEmployeesAsync(int managerId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<EmployeeModel>> GetAllAsync(int managerId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}