using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Office.BLL.Exceptions;
using Office.BLL.Helpers;
using Office.BLL.Models;
using Office.BLL.Services.Interfaces;
using Office.DAL.Entity;
using Office.DAL.Entity.Employees;
using Office.DAL.Entity.Selections;
using Office.DAL.Repositories.Intefaces;

namespace Office.BLL.Services;

public class ProjectService(IProjectRepository projectRepository, IMapper mapper,IEmployeeRepository employeeRepository) : IProjectService
{
    public async Task<ProjectModel> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var employeeDb = await projectRepository.GetByIdAsync(id, cancellationToken);
    
        if (employeeDb is null)
            throw new EntityNotFoundException($"Project with Id {id} not found");

        return mapper.Map<ProjectModel>(employeeDb);
    }

    public async Task<ProjectModel> CreateProjectAsync(ProjectModel projectModel, int managerId,
        CancellationToken cancellationToken = default)
    {
        var creator = await employeeRepository.GetAll()
            .SingleOrDefaultAsync(r => r.Id == managerId && (r is ProjectManager || r is Admin),
                cancellationToken);
        if (creator is null)
            throw new NotPermissionException("You don't have permissions");
        
        projectModel.ProjectManagerId = creator.Id;
        
        var project = await projectRepository.CreateProjectAsync(mapper.Map<Project>(projectModel), cancellationToken);
        return mapper.Map<ProjectModel>(project);
    }

    public async Task DeleteProjectAsync(int projectId, int managerId, CancellationToken cancellationToken = default)
    {
        var creator = await employeeRepository.GetAll().Where(r => r.Id == managerId && (r is ProjectManager || r is Admin))
            .SingleOrDefaultAsync(cancellationToken);
        if (creator is null)
            throw new NotPermissionException("You don't have permissions");
        
        var projectDb = await projectRepository.GetByIdAsync(projectId, cancellationToken);
        
        if (projectDb is null)
            throw new EntityNotFoundException($"Project with Id {projectId} not found");
        await projectRepository.DeleteProjectAsync(projectDb, cancellationToken);
    }

    public async Task<ProjectModel> UpdateProjectAsync(int projectManagerId, ProjectModel projectModel,
        CancellationToken cancellationToken = default)
    {
        var managerDb = await employeeRepository.GetAll()
            .SingleOrDefaultAsync(r => r.Id == projectManagerId && (r is ProjectManager || r is Admin),
                cancellationToken);
        if (managerDb is null)
            throw new NotPermissionException("You don't have permissions");

        var updateProjectManager = await employeeRepository.GetAll()
            .SingleOrDefaultAsync(r => r.Id == projectModel.ProjectManagerId && (r is ProjectManager || r is Admin),
                cancellationToken);
        if (updateProjectManager is null)
            throw new ManagerException(
                $"Project manager or admin with Id {projectModel.ProjectManagerId} not found");

        var projectDb = await projectRepository.GetByIdAsync(projectModel.Id, cancellationToken);
        if (projectDb is null)
            throw new EntityNotFoundException($"Project with Id {projectModel.Id} not found");

        if (managerDb is Admin || managerDb is ProjectManager && projectDb.ProjectManagerId == managerDb.Id)
        {
            foreach (var propertyMap in ReflectionHelper.WidgetUtil<ProjectModel, Project>.PropertyMap)
            {
                var userProperty = propertyMap.Item1;
                var userDbProperty = propertyMap.Item2;

                var userSourceValue = userProperty.GetValue(projectModel);
                var userTargetValue = userDbProperty.GetValue(projectDb);

                if (userSourceValue != null &&
                    !Equals(userSourceValue, new DateTime()) &&
                    !ReferenceEquals(userSourceValue, "") && !userSourceValue.Equals(userTargetValue))
                {
                    userDbProperty.SetValue(projectDb, userSourceValue);
                }
            }

            await projectRepository.UpdateProjectAsync(projectDb, cancellationToken);
            return mapper.Map<ProjectModel>(projectDb);
        }

        throw new NotPermissionException("You don't have permissions");
    }

    public async Task AddEmployeesInProjectAsync(int projectManagerId, int projectId, ICollection<int> employeeModelsIds,
        CancellationToken cancellationToken = default)
    {
        var manager = await employeeRepository.GetAllManagers()
            .SingleOrDefaultAsync(r => r.Id == projectManagerId && (r is ProjectManager || r is Admin),
                cancellationToken);
        if (manager is null)
            throw new NotPermissionException("You don't have permissions");
        
        var employees = await employeeRepository.GetAllEmployees().Include(r => r.Projects)
            .Where(r => employeeModelsIds.Contains(r.Id)).ToListAsync(cancellationToken);

        if (employees.Any())
        {
            var projectDb = await projectRepository.GetByIdAsync(projectId, cancellationToken);
            if (projectDb is null)
                throw new EntityNotFoundException($"Project with Id {projectId} not found");

            projectDb.Employees = employees;

            await projectRepository.UpdateProjectAsync(projectDb, cancellationToken);
        }
        else
        {
            throw new EmployeeNotFoundException($"Employees not found");
        }
    }
    
    public async Task<List<ProjectModel>> GetAllAsync(int userId, CancellationToken cancellationToken = default)
    {
        var userDb = await employeeRepository.GetAll()
            .SingleOrDefaultAsync(r => r.Id == userId, cancellationToken);
        if (userDb is null)
            throw new EntityNotFoundException($"Project manager or admin with Id {userId} not found");

        var projectsDb = userDb switch
        {
            HrManager => await projectRepository.GetAll()
                .Include(r => r.ProjectType)
                .Include(r => r.ProjectManager)
                .Where(r => r.Employees.Any(i => i.HrManagerId == userId))
                .ToListAsync(cancellationToken),

            ProjectManager => await projectRepository.GetAll()
                .Include(r => r.ProjectType)
                .Include(r => r.ProjectManager)
                .Where(r => r.ProjectManagerId == userId)
                .ToListAsync(cancellationToken),

            Employee => await projectRepository.GetAll()
                .Include(r => r.ProjectType)
                .Include(r => r.ProjectManager)
                .Where(r => r.Employees.Any(i => i.Id == userId))
                .ToListAsync(cancellationToken),

            Admin => await projectRepository.GetAll()
                .Include(r => r.ProjectType)
                .Include(r => r.ProjectManager)
                .ToListAsync(cancellationToken),

            _ => throw new EmployeeNotFoundException($"Employee with Id {userId} not found")
        };

        return mapper.Map<List<ProjectModel>>(projectsDb.OrderBy(r=>r.Status));
    }
}