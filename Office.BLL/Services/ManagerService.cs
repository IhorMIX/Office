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
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;
    private readonly IMapper _mapper = mapper;
    
    public async Task<BaseManagerModel> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var managerDb = await _employeeRepository.GetByIdAsync(id, cancellationToken);
        
        if (managerDb is null)
            throw new EmployeeNotFoundException($"Employee with Id {id} not found");
        
        var managerModel = _mapper.Map<BaseManagerModel>(managerDb);
        return managerModel;
    }

    public async Task<BaseManagerModel> CreateProjectManagerAsync(int adminId, BaseManagerModel managerModel, CancellationToken cancellationToken = default)
    {
        var user = await _employeeRepository.GetAllManagers().SingleOrDefaultAsync(r => r.Id == adminId && !(r is ProjectManager), cancellationToken);
        if (user is null)
            throw new ManagerException("Invalid manager type");
        
        var managerDb = await _employeeRepository.GetAll().FirstOrDefaultAsync(i => i.Login == managerModel.Login, cancellationToken);
        if (managerDb is not null)
        {
            if (managerDb.Login == managerModel.Login)
                throw new AlreadyLoginException("Login is already used by another employee");
        }
        
        BaseManager manager;
        switch (managerModel)
        {
            case ProjectManagerModel:
                manager = _mapper.Map<ProjectManager>(managerModel);
                break;
            case HrManagerModel:
                manager = _mapper.Map<HrManager>(managerModel);
                break;
            default:
                throw new Exception();
        }
        
        manager.Password = PasswordHelper.HashPassword(manager.Password);
        var addedManager = await _employeeRepository.AddEmployeeAsync(manager, cancellationToken);
        return _mapper.Map<BaseManagerModel>(addedManager);
    }

    public async Task<BaseManagerModel> UpdateManagerAsync(int managerId, BaseManagerModel managerModel, CancellationToken cancellationToken = default)
    {
        var updater = await _employeeRepository.GetAllManagers().SingleOrDefaultAsync(r => r.Id == managerId && !(r is ProjectManager), cancellationToken);
        if (updater is null)
            throw new ManagerException("Invalid manager type");

        if (updater is Admin || (updater is BaseManager && updater.Id == managerModel.Id))
        {
            var managerDb = await _employeeRepository.GetByIdAsync(managerModel.Id, cancellationToken);
            
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
                
            var updatedManager = await _employeeRepository.UpdateEmployeeAsync(managerDb, cancellationToken);
            return _mapper.Map<BaseManagerModel>(updatedManager);
        }
        throw new ManagerException("Invalid manager type");
    }

    public async Task DeleteManagerAsync(int userId, int managerId, CancellationToken cancellationToken = default)
    {
        if (userId == managerId)
            throw new Exception("You can't delete yourself");
            
        var user = await _employeeRepository.GetAllManagers().SingleOrDefaultAsync(r => r.Id == userId && !(r is ProjectManager), cancellationToken);
        if (user is null)
            throw new ManagerException("Invalid manager type");
        
        var managerDb = await _employeeRepository.GetByIdAsync(managerId, cancellationToken);
        if (managerDb is null)
            throw new EmployeeNotFoundException($"Employee with Id {managerId} not found");

        await _employeeRepository.DeleteEmployeeAsync(managerDb, cancellationToken);
    }

    public async Task<List<BaseManagerModel>> GetAll(int adminId, CancellationToken cancellationToken = default)
    {
        var user = await _employeeRepository.GetAllManagers().SingleOrDefaultAsync(r => r.Id == adminId, cancellationToken);
        if (user is null)
            throw new ManagerException("Invalid manager type");
        
        var managersDb = await _employeeRepository.GetAllManagers().ToListAsync(cancellationToken);
        return _mapper.Map<List<BaseManagerModel>>(managersDb);
    }

    public async Task<List<HrManagerModel>> GetHrManagers(int adminId, CancellationToken cancellationToken = default)
    {
        var user = await _employeeRepository.GetAllManagers().SingleOrDefaultAsync(r => r.Id == adminId, cancellationToken);
        if (user is null)
            throw new ManagerException("Invalid manager type");
        
        var managersDb = await _employeeRepository.GetAllHrManagers().ToListAsync(cancellationToken);
        return _mapper.Map<List<HrManagerModel>>(managersDb);
    }

    public async Task<List<ProjectManagerModel>> GetProjectManagers(int adminId, CancellationToken cancellationToken = default)
    {
        var user = await _employeeRepository.GetAllManagers().SingleOrDefaultAsync(r => r.Id == adminId, cancellationToken);
        if (user is null)
            throw new ManagerException("Invalid manager type");
        
        var managersDb = await _employeeRepository.GetAllProjectManagers().ToListAsync(cancellationToken);
        return _mapper.Map<List<ProjectManagerModel>>(managersDb);
    }
}