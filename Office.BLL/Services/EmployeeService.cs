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
            .SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
    
        if (employeeDb is null)
            throw new EmployeeNotFoundException($"Employee with Id {id} not found");

        return mapper.Map<EmployeeModel>(employeeDb);
    }


    public async Task<EmployeeModel> CreateEmployeeAsync(int managerId, EmployeeModel employeeModel, CancellationToken cancellationToken = default)
    {
        var creator = await employeeRepository.GetByIdAsync(managerId, cancellationToken);
        if (creator is not (HrManager or Admin))
            throw new NotPermissionException("You don't have permissions");
        
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

    public async Task<EmployeeModel> UpdateEmployeeAsync(int managerId, EmployeeModel employeeModel, CancellationToken cancellationToken = default)
    {
        var updater = await employeeRepository.GetByIdAsync(managerId, cancellationToken);
        if (updater is not (HrManager or Admin))
            throw new NotPermissionException("You don't have permissions");

        var employeeDb = await employeeRepository.GetAllEmployees()
            .SingleOrDefaultAsync(r => r.Id == employeeModel.Id, cancellationToken);

        if (employeeDb is null)
            throw new EmployeeNotFoundException($"Employee with Id {employeeModel.Id} not found");

        foreach (var propertyMap in ReflectionHelper.WidgetUtil<EmployeeModel, Employee>.PropertyMap)
        {
            var userProperty = propertyMap.Item1;
            var userDbProperty = propertyMap.Item2;

            var userSourceValue = userProperty.GetValue(employeeModel);
            var userTargetValue = userDbProperty.GetValue(employeeDb);

            if (userSourceValue != null && !ReferenceEquals(userSourceValue, "") &&
                !userSourceValue.Equals(userTargetValue))
            {
                userDbProperty.SetValue(employeeDb, userSourceValue);
            }
        }

        employeeDb!.Password = string.IsNullOrEmpty(employeeModel.Password)
            ? employeeDb.Password
            : PasswordHelper.HashPassword(employeeModel.Password);
        var updatedManager = await employeeRepository.UpdateEmployeeAsync(employeeDb, cancellationToken);
        return mapper.Map<EmployeeModel>(updatedManager);
    }

    public async Task DeleteEmployeeAsync(int id, int managerId, CancellationToken cancellationToken = default)
    {
        var creator = await employeeRepository.GetByIdAsync(managerId, cancellationToken);
        if (creator is not (HrManager or Admin))
            throw new NotPermissionException("You don't have permissions");
        
        var employeeDb = await employeeRepository.GetByIdAsync(id, cancellationToken);
        
        if (employeeDb is null)
            throw new EmployeeNotFoundException($"Employee with Id {id} not found");

        await employeeRepository.DeleteEmployeeAsync(employeeDb, cancellationToken);
    }
    public async Task<List<EmployeeModel>> GetAllAsync(int managerId, CancellationToken cancellationToken = default)
    {
        var creator = await employeeRepository.GetByIdAsync(managerId, cancellationToken);
        if (creator is Employee)
            throw new NotPermissionException("You don't have permissions");

        var employees = await employeeRepository.GetAllEmployees()
            .ToListAsync(cancellationToken);
        
        return mapper.Map<List<EmployeeModel>>(employees.OrderBy(r=>r.Status));
    }
}