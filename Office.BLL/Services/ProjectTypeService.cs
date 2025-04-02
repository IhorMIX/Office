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

    public async Task<ProjectTypeModel> CreateProjectTypeAsync(ProjectTypeModel projectTypeModel, int managerId,
        CancellationToken cancellationToken = default)
    {
        var creator = await employeeRepository.GetAll().Where(r => r.Id == managerId && (r is ProjectManager || r is Admin)).SingleOrDefaultAsync(cancellationToken);
        if (creator is null)
            throw new EmployeeNotFoundException($"Admin or Hr Manager with Id {managerId} not found");
        
        var projectTypeDb = await projectTypeRepository.GetAll().FirstOrDefaultAsync(i => i.Name == projectTypeModel.Name, cancellationToken);
        if (projectTypeDb is not null)
        {
            if (projectTypeDb.Name == projectTypeModel.Name)
                throw new AlreadyDataException("Project Type is already created");
        }
        
        var projectType = await projectTypeRepository.CreateProjectTypeAsync(mapper.Map<ProjectType>(projectTypeModel), cancellationToken);
        return mapper.Map<ProjectTypeModel>(await projectTypeRepository.GetByIdAsync(projectType.Id, cancellationToken));
    }

    public async Task DeleteProjectTypeAsync(int projectTypeId, int managerId, CancellationToken cancellationToken = default)
    {
        var creator = await employeeRepository.GetAll().Where(r => r.Id == managerId && (r is ProjectManager || r is Admin)).SingleOrDefaultAsync(cancellationToken);
        if (creator is null)
            throw new EmployeeNotFoundException($"Admin or Hr Manager with Id {managerId} not found");
        
        var projectTypeDb = await projectTypeRepository.GetByIdAsync(projectTypeId, cancellationToken);
        
        if (projectTypeDb is null)
            throw new EntityNotFoundException($"Project Type with Id {projectTypeId} not found");
        await projectTypeRepository.DeleteProjectTypeAsync(projectTypeDb, cancellationToken);
    }
}