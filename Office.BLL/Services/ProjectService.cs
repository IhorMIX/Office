using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Office.BLL.Exceptions;
using Office.BLL.Models;
using Office.BLL.Services.Interfaces;
using Office.DAL.Entity;
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
        var creator = await employeeRepository.GetAll().Where(r => r.Id == managerId && (r is ProjectManager || r is Admin)).SingleOrDefaultAsync(cancellationToken);
        if (creator is null)
            throw new EmployeeNotFoundException($"Admin or Project Manager with Id {managerId} not found");
        
        var project = await projectRepository.CreateProjectAsync(mapper.Map<Project>(projectModel), cancellationToken);
        return mapper.Map<ProjectModel>(project);
    }

    public async Task DeleteProjectAsync(int projectId, int managerId, CancellationToken cancellationToken = default)
    {
        var creator = await employeeRepository.GetAll().Where(r => r.Id == managerId && (r is ProjectManager || r is Admin)).SingleOrDefaultAsync(cancellationToken);
        if (creator is null)
            throw new EmployeeNotFoundException($"Admin or Project Manager with Id {managerId} not found");
        
        var projectDb = await projectRepository.GetByIdAsync(projectId, cancellationToken);
        
        if (projectDb is null)
            throw new EntityNotFoundException($"Project with Id {projectId} not found");
        await projectRepository.DeleteProjectAsync(projectDb, cancellationToken);
    }

    public Task<ProjectModel> UpdateProjectAsync(int projectManagerId, ProjectModel projectModel, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task AddEmployeesInProjectAsync(int projectManagerId, int projectId, ICollection<int> employeeModelsIds,
        CancellationToken cancellationToken = default)
    {
        
        var manager = await employeeRepository.GetAll()
            .SingleOrDefaultAsync(r => r.Id == projectManagerId && (r is ProjectManager || r is Admin),
                cancellationToken);
        if (manager is null)
            throw new EmployeeNotFoundException($"Manger or Admin with Id {projectManagerId} not found");
        
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
}