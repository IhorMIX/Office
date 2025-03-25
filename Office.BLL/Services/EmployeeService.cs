using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Office.BLL.Exceptions;
using Office.BLL.Helpers;
using Office.BLL.Models;
using Office.BLL.Services.Interfaces;
using Office.DAL.Entity;
using Office.DAL.Entity.Employees;
using Office.DAL.Repositories.Intefaces;


namespace Office.BLL.Services;

public class EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper) : IEmployeeService
{
    public async Task<EmployeeModel> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var employeeDb = await employeeRepository.GetAllEmployees()
            .Include(e => e.Projects)
            .ThenInclude(p => p.ProjectManager)
            .SingleOrDefaultAsync(i => i.Id == id, cancellationToken);
        
        if (employeeDb is null)
            throw new EmployeeNotFoundException($"Employee with Id {id} not found");

        return mapper.Map<EmployeeModel>(employeeDb);
    }

    public async Task<EmployeeModel> CreateEmployeeAsync(int managerId, EmployeeModel employeeModel, CancellationToken cancellationToken = default)
    {
        var creator = await employeeRepository.GetAll().Where(r => r.Id == managerId && (r is HrManager || r is Admin)).SingleOrDefaultAsync(cancellationToken);
        if (creator is null)
            throw new EmployeeNotFoundException($"Manager with Id {managerId} not found");
        
        var employeeDb = await employeeRepository.GetAll().FirstOrDefaultAsync(i => i.Login == employeeModel.Login, cancellationToken);
        if (employeeDb is not null)
        {
            if (employeeDb.Login == employeeModel.Login)
                throw new AlreadyLoginException("Login is already used by another employee");
        }
        
        employeeModel.Password = PasswordHelper.HashPassword(employeeModel.Password);
        if (creator is HrManager)
            employeeModel.HrManagerId = creator.Id;
        employeeDb = await employeeRepository.AddEmployeeAsync(mapper.Map<Employee>(employeeModel), cancellationToken);
        return mapper.Map<EmployeeModel>(await employeeRepository.GetByIdAsync(employeeDb.Id, cancellationToken));
    }

    public Task<EmployeeModel> UpdateEmployeeAsync(int managerId, EmployeeModel employeeModel, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteEmployeeAsync(int id, CancellationToken cancellationToken = default)
    {
        var employeeDb = await employeeRepository.GetByIdAsync(id, cancellationToken);
        
        if (employeeDb is null)
            throw new EmployeeNotFoundException($"Employee with Id {id} not found");

        await employeeRepository.DeleteEmployeeAsync(employeeDb, cancellationToken);
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