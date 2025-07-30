using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Office.BLL.Exceptions;
using Office.BLL.Models;
using Office.BLL.Services.Interfaces;
using Office.DAL.Entity;
using Office.DAL.Entity.Selections;
using Office.DAL.Repositories.Intefaces;

namespace Office.BLL.Services;

public class ProjectTypeService(IProjectTypeRepository projectTypeRepository, IMapper mapper,IEmployeeRepository employeeRepository) : IProjectTypeService
{
    public async Task<ProjectTypeModel> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var projectTypeDb = await projectTypeRepository.GetByIdAsync(id, cancellationToken);
        
        if (projectTypeDb is null)
            throw new EntityNotFoundException($"Project Type with Id {id} not found");
        
        var subdivision = mapper.Map<ProjectTypeModel>(projectTypeDb);
        return subdivision;
    }

    public async Task<List<ProjectType>> GetAllAsync(CancellationToken cancellationToken)
    {
        var projectType = await projectTypeRepository.GetAll().ToListAsync(cancellationToken);
        return projectType;
    }
    
    public async Task<ProjectType> CreateProjectTypeAsync(int managerId, string projectName, CancellationToken cancellationToken = default)
    {
        var creator = await employeeRepository.GetAll().Where(r => r.Id == managerId && (r is ProjectManager || r is Admin))
            .SingleOrDefaultAsync(cancellationToken);
        if (creator is null)
            throw new NotPermissionException("You don't have permissions");
        
        var projectTypeDb = await projectTypeRepository.GetAll().FirstOrDefaultAsync(i => i.Name == projectName, cancellationToken);
        if (projectTypeDb is not null)
        {
            if (projectTypeDb.Name == projectName)
                throw new AlreadyDataException("Project Type is already created");
        }
        
        var projectType = await projectTypeRepository.CreateProjectTypeAsync(new ProjectType
        {
            Name = projectName,
        }, cancellationToken);
        return projectType;
    }

    public async Task DeleteProjectTypeAsync(int projectTypeId, int managerId, CancellationToken cancellationToken = default)
    {
        var creator = await employeeRepository.GetAll().Where(r => r.Id == managerId && (r is ProjectManager || r is Admin))
            .SingleOrDefaultAsync(cancellationToken);
        if (creator is null)
            throw new NotPermissionException("You don't have permissions");
        
        var projectTypeDb = await projectTypeRepository.GetByIdAsync(projectTypeId, cancellationToken);
        
        if (projectTypeDb is null)
            throw new EntityNotFoundException($"Project Type with Id {projectTypeId} not found");
        await projectTypeRepository.DeleteProjectTypeAsync(projectTypeDb, cancellationToken);
    }

    public async Task UpdateProjectTypeAsync(int managerId, ProjectType projectType, CancellationToken cancellationToken = default)
    {
        var managerDb = await employeeRepository.GetAllManagers()
            .SingleOrDefaultAsync(r => r.Id == managerId && !(r is HrManager),
                cancellationToken);
        if (managerDb is null)
            throw new NotPermissionException("You don't have permissions");

        var projectTypeCheck = await projectTypeRepository.GetAll().Where(r => r.Name == projectType.Name)
            .SingleOrDefaultAsync(cancellationToken);
        if (projectTypeCheck != null)
            throw new AlreadyDataException($"ProjectType with name {projectType.Name} created already");
        
        var projectTypeDb = await projectTypeRepository.GetByIdAsync(projectType.Id, cancellationToken);
        if (projectTypeDb is null)
            throw new EntityNotFoundException($"ProjectType with id {projectType.Id} not found");

        projectTypeDb.Name = projectType.Name;

        await projectTypeRepository.UpdateProjectTypeAsync(projectTypeDb, cancellationToken);
    }
}