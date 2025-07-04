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

public class ManagerService(IEmployeeRepository employeeRepository, IMapper mapper) : IManagerService
{
    public async Task<BaseEmployee> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var managerDb = await employeeRepository.GetManagerByIdAsync(id, cancellationToken);
        
        if (managerDb is null)
            throw new ManagerException($"Manager with Id {id} not found");
        return managerDb;
    }

    public async Task<BaseManagerModel> CreateManagerAsync(int adminId, BaseManagerModel managerModel, CancellationToken cancellationToken = default)
    {
        var user = await employeeRepository.GetAllManagers().SingleOrDefaultAsync(r => r.Id == adminId && r is Admin, cancellationToken);
        if (user is null)
            throw new NotPermissionException("You don't have permissions");
        
        var managerDb = await employeeRepository.GetAll().FirstOrDefaultAsync(i => i.Login == managerModel.Login, cancellationToken);
        if (managerDb is not null)
        {
            if (managerDb.Login == managerModel.Login)
                throw new AlreadyLoginException("Login is already used by another employee");
        }
        
        BaseManager manager;
        switch (managerModel)
        {
            case ProjectManagerModel:
                manager = mapper.Map<ProjectManager>(managerModel);
                break;
            case HrManagerModel:
                manager = mapper.Map<HrManager>(managerModel);
                break;
            default:
                throw new Exception();
        }
        
        manager.Password = PasswordHelper.HashPassword(manager.Password);
        var addedManager = await employeeRepository.AddEmployeeAsync(manager, cancellationToken);
        return mapper.Map<BaseManagerModel>(addedManager);
    }

    public async Task<BaseManagerModel> UpdateManagerAsync(int managerId, BaseManagerModel managerModel, CancellationToken cancellationToken = default)
    {
        var updater = await employeeRepository.GetAllManagers().SingleOrDefaultAsync(r => r.Id == managerId && r is Admin, cancellationToken);
        if (updater is null)
            throw new NotPermissionException("You don't have permissions");

        if (updater is Admin || (updater is BaseManager && updater.Id == managerModel.Id))
        {
            var managerDb = await employeeRepository.GetByIdAsync(managerModel.Id, cancellationToken);
            
            foreach (var propertyMap in ReflectionHelper.WidgetUtil<BaseManagerModel, BaseManager>.PropertyMap)
            {
                var userProperty = propertyMap.Item1;
                var userDbProperty = propertyMap.Item2;

                var userSourceValue = userProperty.GetValue(managerModel);
                var userTargetValue = userDbProperty.GetValue(managerDb);

                if (userSourceValue != null && !ReferenceEquals(userSourceValue, "") && !userSourceValue.Equals(userTargetValue))
                {
                    userDbProperty.SetValue(managerDb, userSourceValue);
                }
            }
                
            managerDb!.Password = string.IsNullOrEmpty(managerModel.Password)
                ? managerDb.Password
                : PasswordHelper.HashPassword(managerModel.Password);
                
            var updatedManager = await employeeRepository.UpdateEmployeeAsync(managerDb, cancellationToken);
            return mapper.Map<BaseManagerModel>(updatedManager);
        }
        throw new NotPermissionException("You don't have permissions");
    }
    //removes just employees and manager
    public async Task DeleteManagerAsync(int managerId, int adminId, CancellationToken cancellationToken = default)
    {
        if (managerId == adminId)
            throw new NotPermissionException("You can't delete yourself");
        
        var adminUser = await employeeRepository.GetAllManagers()
            .SingleOrDefaultAsync(r => r.Id == adminId && r is Admin, cancellationToken);
    
        if (adminUser is null)
            throw new NotPermissionException("You don't have permissions");
        
        var managerDb = await employeeRepository.GetByIdAsync(managerId, cancellationToken);
    
        if (managerDb is null)
            throw new EmployeeNotFoundException($"Manager with Id {managerId} not found");
        
        await employeeRepository.DeleteEmployeeAsync(managerDb, cancellationToken);
    }


    public async Task<List<BaseManagerModel>> GetAll(int adminId, CancellationToken cancellationToken = default)
    {
        var user = await employeeRepository.GetAllManagers().SingleOrDefaultAsync(r => r.Id == adminId, cancellationToken);
        if (user is null)
            throw new NotPermissionException("You don't have permissions");
        
        var managersDb = await employeeRepository.GetAllManagers().ToListAsync(cancellationToken);
        return mapper.Map<List<BaseManagerModel>>(managersDb);
    }

    public async Task<List<HrManagerModel>> GetHrManagers(int adminId, CancellationToken cancellationToken = default)
    {
        var user = await employeeRepository.GetAllManagers().SingleOrDefaultAsync(r => r.Id == adminId, cancellationToken);
        if (user is null)
            throw new NotPermissionException("You don't have permissions");
        
        var managersDb = await employeeRepository.GetAllHrManagers().ToListAsync(cancellationToken);
        return mapper.Map<List<HrManagerModel>>(managersDb);
    }

    public async Task<List<ProjectManagerModel>> GetProjectManagers(int adminId, CancellationToken cancellationToken = default)
    {
        var user = await employeeRepository.GetAllManagers().SingleOrDefaultAsync(r => r.Id == adminId, cancellationToken);
        if (user is null)
            throw new NotPermissionException("You don't have permissions");
        
        var managersDb = await employeeRepository.GetAllProjectManagers().ToListAsync(cancellationToken);
        return mapper.Map<List<ProjectManagerModel>>(managersDb);
    }
}