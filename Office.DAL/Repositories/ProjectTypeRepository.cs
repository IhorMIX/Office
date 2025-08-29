using Microsoft.EntityFrameworkCore;
using Office.DAL.Entity.Selections;
using Office.DAL.Repositories.Intefaces;

namespace Office.DAL.Repositories;

public class ProjectTypeRepository(OfficeDbContext officeDbContext) : IProjectTypeRepository
{
    public IQueryable<ProjectType> GetAll()
    {
        return officeDbContext.ProjectTypes.AsNoTracking();
    }

    public async Task<ProjectType?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await officeDbContext.ProjectTypes.SingleOrDefaultAsync(r => r.Id == id, cancellationToken);
    }
    
    public async Task<ProjectType> CreateProjectTypeAsync(ProjectType projectType, CancellationToken cancellationToken = default)
    {
        var entity = await officeDbContext.ProjectTypes.AddAsync(projectType,cancellationToken);
        await officeDbContext.SaveChangesAsync(cancellationToken);
        return entity.Entity;
    }

    public async Task DeleteProjectTypeAsync(ProjectType projectType, CancellationToken cancellationToken = default)
    {
        officeDbContext.ProjectTypes.Remove(projectType);
        await officeDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateProjectTypeAsync(ProjectType projectType, CancellationToken cancellationToken = default)
    {
        officeDbContext.ProjectTypes.Update(projectType);
        await officeDbContext.SaveChangesAsync(cancellationToken);
    }
}