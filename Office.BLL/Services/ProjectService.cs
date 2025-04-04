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
}